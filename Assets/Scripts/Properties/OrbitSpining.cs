using UnityEngine;

public class OrbitSpining : MonoBehaviour
{
    public float speed;

    private float rotation = 0f;

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, GetRotation());
    }

    private float GetRotation()
    {
        rotation = (rotation + speed * Time.fixedDeltaTime) % 360;
        return rotation;
    }
}
