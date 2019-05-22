using UnityEngine;

public class LaunchBase : MonoBehaviour, IPlanetCollider
{
    public SpaceShip spaceShip;
    public GameControl gameControl;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private Vector3 mousePosition;

    public Vector3 Center { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public float Radius { get; set; }

    void Start()
    {
        PlanetCollider.Initalize(this);

        // TODO - line color fix, currently not working
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(position.x, position.y);

        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, mousePosition);
        }
    }

    private void OnMouseDown()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, mousePosition);
        lineRenderer.SetPosition(1, mousePosition);
    }

    private void OnMouseUp()
    {
        if (lineRenderer.enabled == false)
            return;

        var currentPosition = lineRenderer.GetPosition(0);

        lineRenderer.enabled = false;

        var direction = (mousePosition - currentPosition).normalized;
        var power = Vector3.Distance(currentPosition, mousePosition);
        power = power > Constants.MaxPower ? Constants.MaxPower : power;

        if (direction != Vector3.zero)
        {
            var shipObject = Instantiate(spaceShip, currentPosition, Quaternion.identity);
            shipObject.SetSpeedByPower(power / Constants.MaxPower);
            shipObject.SetDirection(direction);

            gameControl.ships += 1;
        }
    }

    public bool WithinBase(Vector3 position)
    {
        return Vector3.Distance(Center, new Vector3(position.x, position.y, 5)) < Radius;
    }
}
