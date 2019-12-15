using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    Vector3 smoothingVelocity;
    public float smoothingTime;
    Vector3 targetPosition;

    Vector2 viewportSize;
    public Vector2 viewPortFactor;
    public bool followX = true;
    public bool followY = true;

    Vector3 CameraPosition;
    void Start()
    {
        CameraPosition = transform.position;
        CameraPosition.x = CameraPosition.y = 0;
    }
    // Update is called once per frame
    void Update()
    {
        viewportSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(Vector2.zero);
        viewportSize.x *= viewPortFactor.x;
        viewportSize.y *= viewPortFactor.y;
        if( followX)
        {
            if(Mathf.Abs(player.position.x - transform.position.x)> viewportSize.x*0.5f)
            {
                targetPosition = player.position + CameraPosition;
                if (!followY)
                {
                    targetPosition.y = transform.position.y;
                }
            }
        }
        if(followY)
        {
            if (Mathf.Abs(player.position.y - transform.position.y) > viewportSize.y * 0.5f)
            {
                targetPosition = player.position + CameraPosition;
                if (!followX)
                {
                    targetPosition.x = transform.position.x;
                }
            }
        }
        targetPosition.z = CameraPosition.z;
        targetPosition.y = Mathf.Clamp(targetPosition.y, 0, targetPosition.y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothingVelocity, smoothingTime);
        
    }

    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;
        Gizmos.DrawCube(transform.position, viewportSize);
    }
}
