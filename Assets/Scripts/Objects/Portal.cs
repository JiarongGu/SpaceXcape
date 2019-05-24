using System;
using UnityEngine;

public class Portal : MonoBehaviour, IObjectCollider, IGravityProvider
{
    public Portal linkedPortal;
    
    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public Func<float> GetGravityField { get; set; }

    public Func<Vector3> GetCenter { get; set; }

    public Func<float> GetRadius { get; set; }
    
    public Action SpaceShipGravityAction { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ObjectCollider.Initalize(this);
        GravityProvider.Initalize(this, 1.2f);
    }

    void Update()
    {
        SpaceShipGravityAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceship != null && linkedPortal != null)
        {
            spaceship.Position = linkedPortal.GetCenter();
        }
    }
}
