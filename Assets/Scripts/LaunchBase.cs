using System;
using UnityEngine;

public class LaunchBase : MonoBehaviour
{
    public SpaceShip spaceShip;
    public AudioClip launchSound;
    public GameControl arrow;

    private SpriteRenderer spriteRenderer;
    private GameControl gameControl;
    private Vector3 mousePosition;
    private LineRenderer lineRenderer;
    private ObjectAudio lanuchSoundAudio;

    void Start()
    {
        ObjectFactory.CreateRigibody(this);
        lanuchSoundAudio = new ObjectAudio(this, launchSound);

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
            lineRenderer.SetPositions(Array.Empty<Vector3>());
            lanuchSoundAudio.AudioSource.Play(0);
            var shipObject = Instantiate(spaceShip, currentPosition, Quaternion.identity);
            shipObject.SetSpeedByPower(power / Constants.MaxPower);
            shipObject.Direction = direction;
            gameControl.Ships += 1;
        }
    }
}
