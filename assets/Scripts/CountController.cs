using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountController : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] consuptionss;
    public static float sumConsuptionAllHouses = 0.0f;
    public Text test;
    public GameObject[] objCubs;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        SumConsuption();
        test.text = sumConsuptionAllHouses.ToString();
    }

    public void SumConsuption()
    {
        objCubs = GameObject.FindGameObjectsWithTag("Cube");
       // Debug.Log(objCubs.Length);
        float sum = 0;
        foreach (GameObject elem in objCubs)
        {
            //Debug.Log(elem.GetComponent<GetInfo>().sumConsuption);
            sum += elem.GetComponent<GetInfo>().sumConsuption;
        }

        sumConsuptionAllHouses = sum;
    }
}
