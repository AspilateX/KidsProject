using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] production;
    public static float multiProductionAllHouses = 0.0f;
    public Text test;
    public Text balans;
    public GameObject[] objCubs;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MultiProduction();
        test.text = multiProductionAllHouses.ToString();
        balans.text = (multiProductionAllHouses *  ScrollViewAdapter.insolation - CountController.sumConsuptionAllHouses*365).ToString();
    }

    public void MultiProduction()
    {
        float multi = 0;
       
        multi += FindObjectOfType<Extract_GetInfo>().GetComponent<Extract_GetInfo>().multiProduction;
       
        multiProductionAllHouses = multi;
    }

}
