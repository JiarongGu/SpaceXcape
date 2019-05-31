using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;

    private GravityForce gravityForce;

    // Start is called before the first frame update
    void Start()
    {
        ObjectFactory.CreateRigibody(this);
        gravityForce = new GravityForce(this, 1.2f);
    }

    void FixedUpdate()
    {
        gravityForce.FixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceship != null && linkedPortal != null)
        {
            spaceship.Position = linkedPortal.gravityForce.Center;
        }
    }
}
