using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string defaultFollow;

    private Vector3 defaultPosition;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        defaultPosition = transform.position;
    }

    void Update()
    {
        var followObject = FindObjectOfType<SpaceShip>();

        if (followObject == null) {
            transform.position = Vector3.SmoothDamp(transform.position, defaultPosition, ref velocity, 0.5f);
            return;
        }

        var position = followObject.transform.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
