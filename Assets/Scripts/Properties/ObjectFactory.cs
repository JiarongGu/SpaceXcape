using UnityEngine;

public static class ObjectFactory
{
    public static void CreateRigibody(MonoBehaviour monoBehaviour)
    {
        var gameObject = monoBehaviour.gameObject;

        // planet and spaceship need to use z=5, as we develop this in 3D-mode
        // planet and spaceship z-scale need to be 0, so we make 3D in 2D
        monoBehaviour.transform.position = new Vector3(
            monoBehaviour.transform.position.x,
            monoBehaviour.transform.position.y,
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