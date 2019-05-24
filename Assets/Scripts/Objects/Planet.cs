using System;
using UnityEngine;

public class Planet : MonoBehaviour, IObjectCollider, IGravityProvider
{
    public float gravity;
    
    public float GravityField { get; set; }

    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    public Func<Vector3> GetCenter { get; set; }

    public Func<float> GetRadius { get; set; }

    void Start()
    {
        ObjectCollider.Initalize(this);
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
