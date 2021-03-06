﻿using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public GameObject expolsion;

    // program value
    private float speed;
    private Vector3 position;
    private Vector3 direction;
    private float rotation;
    private Vector3 velocity = Vector3.zero;
    private Vector3 movement;

    /// <summary>
    /// set/get spaceship direction
    /// </summary>
    public virtual Vector3 Direction
    {
        get => direction;
        set {
            var vector = new Vector3(value.x, value.y, Constants.PlanetLayer);
            var distance = Vector3.Distance(Vector3.zero, vector);

            direction = 1 / distance * vector;
        }

}

    /// <summary>
    /// set/get spaceship speed
    /// </summary>
    public virtual float Speed
    {
        get => speed;
        set
        {
            if (value < minSpeed) speed = minSpeed;
            if (value > maxSpeed) speed = maxSpeed;
            speed = value;
        }
    }


    /// <summary>
    /// get/set spaceship position
    /// </summary>
    /// <param name="position"></param>
    public virtual Vector3 Position
    {
        get => position;
        set => position = new Vector3(value.x, value.y, Constants.PlanetLayer);
    }


    /// <summary>
    ///  add force to direction
    /// </summary>
    /// <param name="force"></param>
    public virtual void AddForce(Vector3 force)
    {
        direction = new Vector3(direction.x + force.x, direction.y + force.y, direction.z).normalized;
    }

    /// <summary>
    /// set speed by power factor float value 0 ~ 1
    /// </summary>
    /// <param name="power"></param>
    public virtual void SetSpeedByPower(float power)
    {
        this.speed = power * maxSpeed;
    }

    /// <summary>
    /// destroy the spaceship
    /// </summary>
    public virtual void Destroy()
    {
        Destroy(Instantiate(expolsion, transform.position, Quaternion.identity), 1f);
        Destroy(gameObject);
    }

    protected virtual void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Constants.PlanetLayer);
        position = transform.position;
    }

    private void Update()
    {
        // update game object
        transform.position = Vector3.SmoothDamp(position, transform.position, ref velocity, 0.1f);

        if (!WithInBoundary()) Destroy();
    }

    private void FixedUpdate()
    {
        if (direction == Vector3.zero)
            return;
        
        // continues update for position and rotation
        movement = GetMovement(direction, speed * Time.fixedDeltaTime);
        position = position + movement;

        var rotation = GetRotation(movement);

        if (rotation != float.NaN)
            transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    private Vector3 GetMovement(Vector3 direction, float distance)
    {
        var factor = direction / Vector3.Distance(Vector3.zero, direction);
        var movement = factor * distance;
        return new Vector3(movement.x, movement.y);
    }

    private float GetRotation(Vector3 movement)
    {
        var offset = movement.y < 0 ? 180 : 0;
        var rotation = offset + Mathf.Atan(movement.x / movement.y) * Mathf.Rad2Deg * -1;
        return rotation;
    }

    protected bool WithInBoundary()
    {
        var bounds = FindObjectOfType<GameBoundary>().GetComponent<Renderer>().bounds;
        var max = bounds.max;
        var min = bounds.min;
        return !(position.y > max.y || position.x > max.x || position.y < min.y || position.x < min.x);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spaceship = collision.gameObject.GetComponent<SpaceShip>();

        if (spaceship != null)
        {
            var diff = spaceship.position - position;

            spaceship.direction = spaceship.direction - direction + diff;
            spaceship.speed = (spaceship.speed + speed) / 2;
        }
    }
}
