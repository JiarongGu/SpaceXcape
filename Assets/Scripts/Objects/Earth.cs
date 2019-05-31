using System.Linq;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public string sceneName;
    public float gravity;
    public Blink blink;

    private GravityForce gravityForce;
    private GameControl gameControl;

    public Vector3 Center => gravityForce.Center;

    void Start()
    {
        ObjectFactory.CreateRigibody(this);

        gravityForce = new GravityForce(this, gravity);
        gameControl = FindObjectOfType<GameControl>();
    }

    void FixedUpdate()
    {
        gravityForce.FixedUpdate();
    }

    public void StartBlink(float duration) {
        blink.StartBlinking(duration);
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
