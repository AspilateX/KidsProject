using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving2 : MonoBehaviour
{
    // Start is called before the first frame update
   public float distance = 0;
   public float speed = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
  
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Vector3.ProjectOnPlane(Camera.main.ScreenPointToRay(mousePosition).direction * distance, Vector3.up * distance);
        objPosition.y = transform.lossyScale.y * transform.localScale.y / 2f;
        transform.position = objPosition;
        Debug.Log(transform.lossyScale);
      
    }
}