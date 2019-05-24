using System;
using System.Linq;
using UnityEngine;

public interface IGravityProvider
{
    Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    float GravityField { get; set; }

    Action SpaceShipGravityAction { get; set; }

    Func<Vector3> GetCenter { get; set; }

    Func<float> GetRadius { get; set; }
    
    Transform transform { get; }
}

public static class GravityProvider
{
    public static void Initalize(IGravityProvider provider, float gravity)
    {
        var sphereCollider = provider.transform.GetComponent<SphereCollider>();

        if (sphereCollider == null)
            throw new Exception("gravity provider must has sphere collider as centre");

        // get centre and radius based on sphere collider
        provider.GetCenter = () => provider.transform.position + sphereCollider.center;
        provider.GetRadius = () => provider.transform.localScale.x * sphereCollider.radius;

        provider.GravityField = provider.GetRadius() * 3 * (gravity > 0 ? gravity : 1);

        provider.GetGravityForce = (position, distance) =>
            GetGravityForce(provider, position, distance);

        provider.SpaceShipGravityAction = () =>
            SpaceshipGravityAction(provider);

    }

    private static void SpaceshipGravityAction(IGravityProvider gravityProvider)
    {
        var spaceShips = UnityEngine.Object.FindObjectsOfType<SpaceShip>();

        spaceShips
            .Select(x => (
                spaceShip: x,
                position: x.transform.position,
                distance: Vector3.Distance(x.transform.position, gravityProvider.GetCenter())
             ))
            .Where(x => x.distance < gravityProvider.GravityField).ToList()
            .ForEach(x =>
                x.spaceShip.AddForce(gravityProvider.GetGravityForce(x.position, x.distance))
            );
    }

    private static Vector3 GetGravityForce(IGravityProvider provider, Vector3 position, float distance)
    {
        return Vector3.MoveTowards(position, provider.GetCenter(), GetForce(provider.GetRadius(), distance)) - position;
    }

    private static float GetForce(float radius, float distance)
    {
        // force calculation based on newton + modified
        return distance / Mathf.Sqrt(radius) / 25 * Time.fixedDeltaTime;
    }
}