using UnityEngine;

public class OrbitMotion : MonoBehaviour, IObjectCollider
{
    public Vector3 Center { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ObjectCollider.Initalize(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
