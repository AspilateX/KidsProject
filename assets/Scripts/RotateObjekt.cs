using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjekt : MonoBehaviour
    
{
    void Update()
    {
        if (Input.GetKey(KeyCode.F2))
            transform.rotation *= Quaternion.Euler(0f, 50f * Time.deltaTime, 0f);
        if (Input.GetKey(KeyCode.F3))
            transform.rotation *= Quaternion.Euler(0f, -50f * Time.deltaTime, 0f);
    }
}
