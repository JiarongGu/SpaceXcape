using UnityEngine;

public class Station : MonoBehaviour, IObjectCollider
{
    public GameObject stationArraivedAnimation;

    public Vector3 Center { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float Radius { get; set; }

    public bool Collected { get; private set; }

    private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
        ObjectCollider.Initalize(this);
    }

    // Update is called once per frame
    void Update()
    {

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
