using System;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoFollow = true;
    
    private GameControl gameControl;
    private Vector3 defaultPosition;
    private float height;
    private float width;
    private Vector3 velocity = Vector3.zero;
    private Type followObject = typeof(SpaceShip);

    void Start()
    {
        defaultPosition = transform.position;
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        gameControl = FindObjectOfType<GameControl>();
    }

    void FixedUpdate()
    {
        if (!autoFollow)
            return;

        var follow = FindObjectsOfType(followObject)
            .Select(x => (MonoBehaviour)x)
            .OrderBy(x => Vector3.Distance(x.transform.position, gameControl.earth.Center))
            .FirstOrDefault();

        var position = follow != null ? follow.transform.position : defaultPosition;
        UpdateCameraPosition(position);
    }

    private void UpdateCameraPosition(Vector3 position)
    {
        var bounds = gameControl.gameBoundary.GetComponent<Renderer>().bounds;

        var newPosition = new Vector3(
            Mathf.Clamp(position.x, bounds.min.x + width / 2, bounds.max.x - width / 2),
            Mathf.Clamp(position.y, bounds.min.y + height / 2, bounds.max.y - height / 2),
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.2f);
    }

    public void Follow<TObject>() where TObject : MonoBehaviour
    {
        followObject = typeof(TObject);
    }
}
