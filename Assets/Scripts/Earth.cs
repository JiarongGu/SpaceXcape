using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour, IPlanetCollider
{
    public string sceneName;

    public Vector3 Center { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float Radius { get; set; }

    void Start()
    {
        PlanetCollider.Initalize(this);
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneName);
    }
}
