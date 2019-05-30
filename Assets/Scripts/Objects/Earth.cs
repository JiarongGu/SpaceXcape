using System.Linq;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public string sceneName;
    public float gravity;
    public GameControl gameControl;

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
                gameControl.LoadScene(sceneName);
        }
    }
}
