using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int (100,100);
    private Bilding[,] grid;
   // public bool [,] gridControl;

    private Bilding flyingBilding;
    
    private Camera mainCamera;
    // Start is called before the first frame update
    private void Start()
    {
        //gridControl = GameObject.FindObjectOfType<GridControl>().worldGrid;
        /*foreach(bool elem in gridControl)
        {
            Debug.Log(gridControl);
        }*/
        
        grid = new Bilding[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
    }
    public void PlacingBilding(Bilding bildPrefab)
    { if (flyingBilding!=null)
        {
            Destroy(flyingBilding.gameObject);
        }
        flyingBilding = Instantiate(bildPrefab);
    }
    // Update is called once per frame
    private void Update()
    {
        
        
    

       FlyToPlace(flyingBilding);
    



    }

    public void FlyToPlace(Bilding flyingBilding)
    {
        if (flyingBilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;
                if (x < 0 || x > GridSize.x - flyingBilding.Size.x) available = false;
                if (y < 0 || y > GridSize.y - flyingBilding.Size.y) available = false;

                
                if (available && IsPlaceTaken(x, y)) available = false;

                flyingBilding.transform.position = new Vector3(x, flyingBilding.transform.position.y, y);
                flyingBilding.SetTransparent(available);



                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBilding(x, y);
                }
            }
        }
    }


    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < 1; y++)
            {
               if (grid[placeX + x, placeY + y] !=null) return true;
                //if (gridControl[placeX + x, placeY + y] != false) return true;
            }
        }
        return false;
    }
    private void PlaceFlyingBilding(int placeX, int placeY)
    {
        for (int x=0; x< flyingBilding.Size.x; x++)
        {
        for (int y = 0; y< flyingBilding.Size.y; y++)
            {

               grid[placeX + x, placeY + y] = flyingBilding;
                //gridControl[placeX + x, placeY + y]= true;
               
            }
        }
        flyingBilding.SetNormal();
        flyingBilding = null;
    }

    /*public void DeliteDataInGrid(int placeX, int placeY)
    {
        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                try
                {
                    if (!((x < 0 || x > GridSize.x - 1) || (y < 0 || y > GridSize.y - 1)))
                    {
                       grid[placeX + x, placeY + y] = null;
                    //    gridControl[placeX + x, placeY + y] = false;
                    }
                }
                finally
                {
                    Debug.Log("Вышли");

                }


            }
        }
    }*/
}
