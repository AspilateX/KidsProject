﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private const int LevelArea = 100;

    private const int ScrollArea = 25;
    private const int ScrollSpeed = 200;
    private const int DragSpeed = 10;

    private const int ZoomSpeed = 25;
    private int ZoomMin = 25;
    private int ZoomMax = 100;
    // Update is called once per frame
    void Update()
    {
        // Init camera translation for this frame.
        var translation = Vector3.zero;

        // Zoom in or out
        var zoomDelta = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Time.deltaTime;
        if (zoomDelta != 0)
        {
            translation -= Vector3.up * ZoomSpeed * zoomDelta;
        }



        // Move camera with arrow keys
        translation += new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * DragSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * DragSpeed);
/*
        // Move camera with mouse
        if (Input.GetMouseButton(2)) // MMB
        {

        }
        else
        {
            // Move camera if mouse pointer reaches screen borders
            if (Input.mousePosition.x < ScrollArea)
            {
                translation += Vector3.left * ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.x >= Screen.width - ScrollArea)
            {
                translation += Vector3.right * ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y < ScrollArea)
            {
                translation += Vector3.forward * -ScrollSpeed * Time.deltaTime;
            }

            if (Input.mousePosition.y > Screen.height - ScrollArea)
            {
                translation += Vector3.forward * ScrollSpeed * Time.deltaTime;
            }
        }
*/
        // Keep camera within level and zoom area
        var desiredPosition = GetComponent<Camera>().transform.position + translation;

        if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
        {
            translation.x = 0;
        }
        if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
        {
            translation.y = 0;
        }
        if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
        {
            translation.z = 0;
        }

        // Finally move camera parallel to world axis
        GetComponent<Camera>().transform.position += translation;
    }
}
 


