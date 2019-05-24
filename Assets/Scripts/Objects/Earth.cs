using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour, IObjectCollider, IGravityProvider
{
    public string sceneName;
    public float gravity;
    
    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public Func<float> GetGravityField { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    public Func<Vector3> GetCenter { get; set; }

    public Func<float> GetRadius { get; set; }

    void Start()
    {
        ObjectCollider.Initalize(this);
        GravityProvider.Initalize(this, gravity);
    }

    void Update()
    {
        SpaceShipGravityAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var stations = FindObjectsOfType<Station>();

        if (!stations.Any(x => x.Collected == false))
        {
            if (collision.gameObject.GetComponent<SpaceShip>() != null)
                SceneManager.LoadScene(sceneName);
        }
    }
}
