using System;
using UnityEngine;

public class OrbitMotion : MonoBehaviour, IObjectCollider, IGravityProvider
{
    public float speed;
    public float gravity;
    public float radius;

    private Vector3 startPosition;
    private float orbitProgress = 0;
    private SphereCollider sphereCollider;

    public Vector3 Center { get => transform.position + sphereCollider.center; set { } }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

    public float GravityField { get; set; }

    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    void Start()
    {
        startPosition = transform.position;
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        ObjectCollider.Initalize(this);
        GravityProvider.Initalize(this, gravity);
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitProgress > 1)
            orbitProgress = 0;

        orbitProgress = orbitProgress + speed * Time.fixedDeltaTime;

        transform.position = GetMovement(orbitProgress);
        SpaceShipGravityAction();
    }

    Vector3 GetMovement(float turn) {
        float angle = Mathf.Deg2Rad * 360f * turn;
        float x = Mathf.Sin(angle) * radius + startPosition.x;
        float y = Mathf.Cos(angle) * radius + startPosition.y;
        return new Vector3(x, y, Constants.PlanetLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();
        if (spaceship != null)
        {
            spaceship.Destroy();
        }
    }
}
