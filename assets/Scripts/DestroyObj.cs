using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class DestroyObj : MonoBehaviour 
{

    private void OnMouseOver()
    {
        {
            if (Input.GetKey(KeyCode.Delete))
                Destroy(this.gameObject);
        }
    }
}
