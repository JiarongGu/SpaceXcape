using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour, IObjectCollider
{
    public GameObject stationArraivedAnimation;

    public Vector3 Center { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float Radius { get; set; }

    public bool Collected { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
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
            Collected = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(Instantiate(stationArraivedAnimation, transform.position, Quaternion.identity), 0.5f);
        }
    }
}
