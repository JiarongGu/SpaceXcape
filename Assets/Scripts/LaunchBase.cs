using UnityEngine;

public class LaunchBase : MonoBehaviour
{
    public SpaceShip spaceShip;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private GameControl gameControl;
    private Vector3 mousePosition;
    
    void Start()
    {
        ObjectFactory.CreateRigibody(this);
        gameControl = FindObjectOfType<GameControl>();
        UpdateMousePosition();

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
        UpdateMousePosition();
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, mousePosition);
        }
    }

    private void UpdateMousePosition() {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(position.x, position.y);
    }

    private void OnMouseDown()
    {
        if (enabled == false)
            return;

        if (lineRenderer == null)
            return;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, mousePosition);
        lineRenderer.SetPosition(1, mousePosition);
    }

    private void OnMouseUp()
    {
        if (lineRenderer == null)
            return;

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
            shipObject.Direction = direction;

            gameControl.Ships += 1;
        }
    }
}
