using System;
using System.Linq;
using UnityEngine;

public interface IGravityProvider
{
    Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    Vector3 Center { get; set; }

    float Radius { get; set; }

    float GravityField { get; set; }

    Action SpaceShipGravityAction { get; set; }
}

public static class GravityProvider
{
    public static void Initalize(IGravityProvider gravityProvider, float gravity)
    {
        gravityProvider.GravityField = gravityProvider.Radius * 3 * (gravity > 0 ? gravity : 1);

        gravityProvider.GetGravityForce = (position, distance) =>
            GetGravityForce(gravityProvider.Center, gravityProvider.Radius, position, distance);

        gravityProvider.SpaceShipGravityAction = () =>
            SpaceshipGravityAction(gravityProvider);

    }

    private static void SpaceshipGravityAction(IGravityProvider gravityProvider)
    {
        var spaceShips = UnityEngine.Object.FindObjectsOfType<SpaceShip>();

        spaceShips
            .Select(x => (
                spaceShip: x,
                position: x.transform.position,
                distance: Vector3.Distance(x.transform.position, gravityProvider.Center)
             ))
            .Where(x => x.distance < gravityProvider.GravityField).ToList()
            .ForEach(x =>
                x.spaceShip.AddForce(gravityProvider.GetGravityForce(x.position, x.distance))
            );
    }

    private static Vector3 GetGravityForce(Vector3 center, float radius, Vector3 position, float distance)
    {
        return Vector3.MoveTowards(position, center, GetForce(radius, distance)) - position;
    }

    private static float GetForce(float radius, float distance)
    {
        // force calculation based on newton + modified
        return distance / Mathf.Sqrt(radius) / 1000;
    }
}