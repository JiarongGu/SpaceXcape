using UnityEngine;

public class Meteorite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectFactory.CreateRigibody(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceship != null)
        {
            spaceship.Direction = spaceship.Direction + spaceship.Position - transform.position;
        }
    }
}
