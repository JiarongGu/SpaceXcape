using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float gravity;

    private GravityForce gravityForce;

    void Start()
    {
        ObjectFactory.CreateRigibody(this);
        gravityForce = new GravityForce(this, gravity);
    }

    void FixedUpdate()
    {
        gravityForce.FixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        // TODO - explode effect apply to ship
        // destroy ship on collision
        if (spaceship != null)
        {
            spaceship.Destroy();
        }
    }
}
