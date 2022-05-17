using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        distance = gameObject.transform.position.z - Camera.main.transform.position.z;


    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        objPosition.y = 0;
    
        transform.position = objPosition;

        
    }
}