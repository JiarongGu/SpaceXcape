using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameBoundary gameBoundary;

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
        var followObject = FindObjectOfType<SpaceShip>();

        if (followObject == null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, defaultPosition, ref velocity, 0.5f);
            return;
        }

        var position = followObject.transform.position;
        var bounds = gameBoundary.GetComponent<Renderer>().bounds;

        transform.position = new Vector3(
            Mathf.Clamp(position.x, bounds.min.x + width / 2, bounds.max.x - width / 2),
            Mathf.Clamp(position.y, bounds.min.y + height / 2, bounds.max.y - height /2),
            transform.position.z
        );
    }
}
