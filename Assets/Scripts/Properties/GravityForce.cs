using System;
using System.Linq;
using UnityEngine;

public class GravityForce
{
    private readonly static float GravityFactor = 3;
    private readonly MonoBehaviour _monoBehaviour;
    private readonly SphereCollider _sphereCollider;
    private readonly float _gravity;

    public GravityForce(MonoBehaviour monoBehaviour, float gravity = 1f)
    {
        _monoBehaviour = monoBehaviour;
        _sphereCollider = monoBehaviour.GetComponent<SphereCollider>();
        _gravity = gravity;

        if (_sphereCollider == null)
            throw new Exception("gravity provider must has sphere collider as centre");
    }

    public Vector3 Center => _monoBehaviour.transform.position + _sphereCollider.center;

    public float Radius => _monoBehaviour.transform.localScale.x * _sphereCollider.radius;

    public float GravityField => Radius * GravityFactor * _gravity;
    
    public void Update()
    {
        var spaceShips = UnityEngine.Object.FindObjectsOfType<SpaceShip>();

        spaceShips
            .Select(x => (
                spaceShip: x,
                position: x.transform.position,
                distance: Vector3.Distance(x.transform.position, Center)
             ))
            .Where(x => x.distance < GravityField).ToList()
            .ForEach(x =>
                x.spaceShip.AddForce(GetGravityForce(x.position, x.distance))
            );
    }

    private Vector3 GetGravityForce(Vector3 position, float distance)
    {
        return Vector3.MoveTowards(position, Center, GetForce(Radius, distance)) - position;
    }

    private float GetForce(float radius, float distance)
    {
        // force calculation based on newton + modified
        return distance / Mathf.Sqrt(radius) / 15 * Time.fixedDeltaTime;
    }
}