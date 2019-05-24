using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour
{
    public string sceneName;
    public float gravity;

    private GravityForce gravityForce;

    public Vector3 Center => gravityForce.Center;

    void Start()
    {
        ObjectFactory.CreateRigibody(this);
        gravityForce = new GravityForce(this, gravity);
    }

    void Update()
    {
        gravityForce.Update();
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
