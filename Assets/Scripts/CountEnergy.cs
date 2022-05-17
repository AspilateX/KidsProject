using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountEnergy : MonoBehaviour
{
    public float[] production;
    public static float multiProductionAllHouses = 0.0f;
    public Text test;
    public Text balans;

    void Update()
    {
        MultiProduction();
        test.text = multiProductionAllHouses.ToString();
        balans.text = (multiProductionAllHouses *  ScrollViewAdapter.insolation - CountController.sumConsuptionAllHouses*365).ToString();
    }

    public void MultiProduction()
    {
        float multi = 0;
       
        //multi += FindObjectOfType<Extract_GetInfo>().multiProduction;
       
        multiProductionAllHouses = multi;
    }

}
