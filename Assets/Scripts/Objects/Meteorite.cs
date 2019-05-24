using UnityEngine;

public class Meteorite : MonoBehaviour, IObjectCollider
{
    public Meteorite crashMeteorite;
    
    // Start is called before the first frame update
    void Start()
    {
        ObjectCollider.Initalize(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceship != null && crashMeteorite != null)
        {
            spaceship.Direction = new Vector3(1, 1) - spaceship.Direction;
        }
    }
}
