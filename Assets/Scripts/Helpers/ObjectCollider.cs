using UnityEngine;

public interface IObjectCollider
{
    GameObject gameObject { get; }

    Transform transform { get; }
}

public static class ObjectCollider
{
    public static void Initalize(IObjectCollider collider)
    {
        var gameObject = collider.gameObject;

        // planet and spaceship need to use z=5, as we develop this in 3D-mode
        // planet and spaceship z-scale need to be 0, so we make 3D in 2D
        collider.transform.position = new Vector3(
            collider.transform.position.x,
            collider.transform.position.y,
            Constants.PlanetLayer
        );

        var rigibody = gameObject.GetComponent<Rigidbody>();

        if (rigibody == null)
        {
            // add rigid body for collision detection
            rigibody = gameObject.AddComponent<Rigidbody>();
            rigibody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}