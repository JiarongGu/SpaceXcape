using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour, IPlanetCollider
{
    public float gravity;
    public Vector3 Center { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float Radius { get; set; }

    void Start()
    {
        PlanetCollider.Initalize(this);
        gravity = Radius * 3 * (gravity > 0 ? gravity : 1);
    }

    void Update()
    {
        var spaceShips = FindObjectsOfType<SpaceShip>();

        spaceShips
            .Select(x => (
                forcable: x,
                position: x.transform.position,
                distance: Vector3.Distance(x.transform.position, Center)
             ))
            .Where(x => x.distance < gravity).ToList()
            .ForEach(x => x.forcable.AddForce(GetGravityForce(x.position, x.distance)));
    }

    private Vector3 GetGravityForce(Vector3 position, float distance)
    {
        return Vector3.MoveTowards(position, Center, GetForce(distance)) - position;
    }

    float GetForce(float distance)
    {
        // force calculation based on newton + modified
        return distance / Mathf.Sqrt(Radius) / 1000;
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
