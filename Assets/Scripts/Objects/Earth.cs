﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour, IObjectCollider, IGravityProvider
{
    public string sceneName;
    public float gravity;

    public Vector3 Center { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public float Radius { get; set; }

    public Func<Vector3, float, Vector3> GetGravityForce { get; set; }

    public float GravityField { get; set; }

    public Action SpaceShipGravityAction { get; set; }

    void Start()
    {
        ObjectCollider.Initalize(this);
        GravityProvider.Initalize(this, gravity);
    }

    void Update() {
        SpaceShipGravityAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var stations = FindObjectsOfType<Station>();

        if (!stations.Any(x => x.Collected == false))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}