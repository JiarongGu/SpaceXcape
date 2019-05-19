using Assets.Scripts.Helpers;
using System;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour, IPlanetCollider, IGravityProvider
{
    public float gravity;

    public Vector3 Center { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

    public float GravityField { get; set; }

    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    void Start()
    {
        PlanetCollider.Initalize(this);
        GravityProvider.Initalize(this, gravity);
    }

    void Update()
    {
        SpaceShipGravityAction();
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
