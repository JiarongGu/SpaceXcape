using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject stationArraivedAnimation;

    private Color defaultColor;

    public bool Collected { get; private set; }

    void Start()
    {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
        ObjectFactory.CreateRigibody(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceShip = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceShip != null)
        {
            Collected = !Collected;
            gameObject.GetComponent<SpriteRenderer>().color = Collected ? Color.white : defaultColor;
            Destroy(Instantiate(stationArraivedAnimation, transform.position, Quaternion.identity), 0.5f);
        }
    }
}
