using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour, IPlanetCollider, IGravityProvider
{
    public string sceneName;
    public float gravity;

    public Vector3 Center { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public float GravityField { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    void Start()
    {
        PlanetCollider.Initalize(this);
        GravityProvider.Initalize(this, gravity);
    }

    void Update() {
        SpaceShipGravityAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneName);
    }
}
