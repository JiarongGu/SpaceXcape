using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public interface IPlanetCollider
    {
        Vector3 Center { get; set; }
        Rigidbody Rigidbody { get; set; }
        float Radius { get; set; }
        Transform transform { get; }
        GameObject gameObject { get; }
    }

    public static class PlanetCollider
    {
        public static void Initalize(IPlanetCollider collider)
        {
            var transform = collider.transform;
            var gameObject = collider.gameObject;
            var sphereCollider = transform.GetComponent<SphereCollider>();

            if (sphereCollider == null)
            {
                throw new Exception("planet must has sphere collider as centre");
            }

            // planet and spaceship need to use z=5, as we develop this in 3D-mode
            // planet and spaceship z-scale need to be 0, so we make 3D in 2D
            transform.position = new Vector3(transform.position.x, transform.position.y, Constants.PlanetLayer);

            // get centre and radius based on sphere collider
            collider.Center = transform.position + sphereCollider.center;
            collider.Radius = transform.localScale.x * sphereCollider.radius;

            var rigibody = gameObject.GetComponent<Rigidbody>();

            if (rigibody == null)
            {
                // add rigid body for collision detection
                collider.Rigidbody = gameObject.AddComponent<Rigidbody>();
                collider.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                collider.Rigidbody = rigibody;
            }

        }
    }
}
