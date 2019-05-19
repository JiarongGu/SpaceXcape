using Assets.Scripts;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public SpaceShip spaceShip;
    public float speed;
    public Vector3 position;
    public GameControl gameControl;
    public LaunchBase launchBase;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        // TODO - line color fix, currently not working
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;

        position = transform.position;
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var currentPosition = new Vector3(mousePosition.x, mousePosition.y);

        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, currentPosition);
        }

        // TODO - restrict start location
        // set start point
        if (Input.GetMouseButtonDown(0))
        {
            if (launchBase == null || launchBase.WithinBase(currentPosition))
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, currentPosition);
                lineRenderer.SetPosition(1, currentPosition);

                position = currentPosition;
                transform.position = position;
            }
        }

        // set end point and speed to shoot
        if (Input.GetMouseButtonUp(0) && lineRenderer.enabled == true)
        {
            lineRenderer.enabled = false;

            var direction = (currentPosition - position).normalized;
            speed = Vector3.Distance(position, currentPosition);

            if (direction != Vector3.zero)
            {
                var ship = Instantiate(spaceShip, position, Quaternion.identity);
                ship.SetSpeed(speed);
                ship.SetDirection(direction);
                gameControl.ships += 1;
            }
        }
    }
}
