using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public interface IForcableObject
    {
        Vector3 Position { get; }
        void AddForce(Vector3 force);
    }
}
