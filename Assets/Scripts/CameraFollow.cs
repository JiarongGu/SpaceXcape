using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameControl gameControl;

    private Vector3 defaultPosition;

    private float height;

    private float width;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        defaultPosition = transform.position;
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Update()
    {
        var sapceShips = FindObjectsOfType<SpaceShip>().OrderBy(x => Vector3.Distance(x.transform.position, gameControl.earth.Center));
        var spaceShip = sapceShips.FirstOrDefault();


        if (spaceShip == null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, defaultPosition, ref velocity, 0.5f);
            return;
        }

        var position = spaceShip.transform.position;
        var bounds = gameControl.gameBoundary.GetComponent<Renderer>().bounds;

        var newPosition = new Vector3(
            Mathf.Clamp(position.x, bounds.min.x + width / 2, bounds.max.x - width / 2),
            Mathf.Clamp(position.y, bounds.min.y + height / 2, bounds.max.y - height / 2),
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, Time.fixedDeltaTime);
    }
}
