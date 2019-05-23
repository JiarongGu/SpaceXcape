using System;
using UnityEngine;

public class meteorite : MonoBehaviour, IObjectCollider
{
    public meteorite crashMeteorite;

    public Vector3 Center { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

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
