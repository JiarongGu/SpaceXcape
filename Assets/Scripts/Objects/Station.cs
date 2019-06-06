using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject stationArraivedAnimation;

    private Color defaultColor;

    public bool Collected { get; private set; } = true;

    void Start()
    {
        defaultColor = new Color(0.15f, 1, 1);
        ObjectFactory.CreateRigibody(this);

        Invoke(nameof(Initalize), 1.5f);
    }

    void Initalize() {
        Collected = false;
        Destroy(Instantiate(stationArraivedAnimation, transform.position, Quaternion.identity), 0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
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
