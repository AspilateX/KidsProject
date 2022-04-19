using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public Text TextLabel;
    // Update is called once per frame
    void Update()
    {
        TextLabel.text = MyDataBase.ExecuteQueryWithAnswer($"SELECT Name_Geo FROM Geo WHERE id_Geo = 74;")+' ';
    }
}
