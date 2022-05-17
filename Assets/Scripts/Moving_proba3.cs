using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_proba3 : MonoBehaviour
{
    public float movingSpeed = 10f;
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            Quaternion rotation = transform.rotation;
            transform.position += rotation * Vector3.forward * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            Quaternion rotation = transform.rotation;
            transform.position += rotation * Vector3.left * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            Quaternion rotation = transform.rotation;
            transform.position += rotation * Vector3.right * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            Quaternion rotation = transform.rotation;
            transform.position += rotation * Vector3.back * movingSpeed * Time.deltaTime;
        }

    }

}
