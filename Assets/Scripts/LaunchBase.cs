using System;
using UnityEngine;

public class LaunchBase : MonoBehaviour
{
    public SpaceShip spaceShip;
    public AudioClip launchSound;
    public Animator spaceShipAnimation;

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
        lineRenderer.sortingLayerName = "Line";

        EnableSpaceShipAnimation(false);
    }

    void Update()
    {
        UpdateMousePosition();

        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(1, mousePosition);

            var rotation = GetRotation(mousePosition - transform.position);
            spaceShipAnimation.transform.eulerAngles = new Vector3(0, 0, rotation);
        }
    }

    private void OnMouseEnter()
    {
        if (enabled) {
            EnableSpaceShipAnimation(true);
            spaceShipAnimation.transform.eulerAngles = new Vector3(0, 0, -45);
        }
    }

    private void OnMouseExit()
    {
        if (!lineRenderer.enabled)
        {
            EnableSpaceShipAnimation(false);
        }
    }

    private void UpdateMousePosition() {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(position.x, position.y);
        spaceShipAnimation.transform.position = position;
    }

    private float GetRotation(Vector3 movement)
    {
        var offset = movement.y < 0 ? 180 : 0;
        var rotation = offset + Mathf.Atan(movement.x / movement.y) * Mathf.Rad2Deg * -1;
        return rotation;
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

        EnableSpaceShipAnimation(false);
    }

    private void EnableSpaceShipAnimation(bool enable) {
        spaceShipAnimation.enabled = enable;
        spaceShipAnimation.GetComponent<Renderer>().enabled = enable;
        Cursor.visible = !enable;
    }
}
