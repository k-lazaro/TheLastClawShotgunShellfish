using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// To type the next 4 lines, start by typing /// and then Tab.
/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [ 0, 0, 0 ].
/// </summary>

public class BoundsCheck : MonoBehaviour
{

    [Header("Set in Inspector")]

    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]

    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    private Camera camera;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Start()
    {
        camera = Camera.main;
    }

    // Called after Update()
    void LateUpdate()
    {

        camHeight = camera.orthographicSize;
        camWidth = camHeight * camera.aspect;

        // Based on the size the camera, check to see if this GameObject is within the camera view

        Vector2 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false;

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }


        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }


        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {                                
            transform.position = pos;                                       
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }

        transform.position = pos;

    }


    // Draw the bounds in the Scene pane using OnDrawGizmos()

    void OnDrawGizmos()
    { 

        if (!Application.isPlaying) return;

        Vector2 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector2.zero, boundSize);

    }

}
