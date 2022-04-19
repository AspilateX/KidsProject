using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour

{
    public Camera camera1;
    public Camera camera2;

    private bool camState = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camState = !camState;
            ChangeCameraState(camState);
        }
    }

    private void ChangeCameraState(bool newState)
    {
        //Swap enabled state to opposite one provided that only is on at a time
        camera1.gameObject.SetActive(!newState);
        camera2.gameObject.SetActive(newState);
    }
}
