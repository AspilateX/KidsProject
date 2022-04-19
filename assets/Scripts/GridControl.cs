using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    public bool [,] worldGrid;
    
    // Start is called before the first frame update
    void Awake()
    {
        worldGrid = new bool[100, 100];
        {
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
            {
                worldGrid[x, y] = false;
            }
        }
    }

    // Update is called once per frame
    public void UpdateGrid()
    {
        GameObject [] objCubs = GameObject.FindGameObjectsWithTag("Cube");
        
       
        foreach (GameObject elem in objCubs)
        {

            worldGrid[Mathf.RoundToInt(elem.transform.position.x), Mathf.RoundToInt(elem.transform.position.y)] = true;
        }
    }
}
