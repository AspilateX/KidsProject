using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilding : MonoBehaviour
{
    public Renderer MainRenderer;

    public Vector2Int Size = Vector2Int.one;
    private Camera mainCamera;
    public BildingsGrid fly;
    

    private void Start()
    {
        mainCamera = Camera.main;
       // fly = GameObject.FindWithTag("Plane").GetComponent<BildingsGrid>();
    }
    public void SetTransparent(bool available)
    {
        if (available)
        { MainRenderer.material.color = Color.green; }
        else
        { MainRenderer.material.color = Color.red; }

    }
    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0.88f, 1, 0, 0.3f);
                Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                Gizmos.DrawCube(pos+ new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }

    }

   /*private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(2))
              {
            int x = Mathf.RoundToInt(Mathf.RoundToInt(transform.position.x));
            int y = Mathf.RoundToInt(Mathf.RoundToInt(transform.position.z));
            fly.DeliteDataInGrid(x, y);
        }
    }
    private void OnMouseDrag()
    {
        {
            

                
                fly.FlyToPlace(this);
                Debug.Log("Перемещаем");
            
            /*var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);




                transform.position = new Vector3(x, transform.position.y, y);




            }

        }
    }*/

}


