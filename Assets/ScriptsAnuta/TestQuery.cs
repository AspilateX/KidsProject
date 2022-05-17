using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TestQuery : MonoBehaviour
{
    private async void Start()
    {
        /*DataTable table_scenes = MyDataBase.GetTable("SELECT * FROM Geo;");
        foreach (DataRow row in table_scenes.Rows)
        {
            foreach (DataColumn column in table_scenes.Columns)
            {
                Debug.Log(row[column]);
            }
        }
        */
        string answer = await MyDataBase.ExecuteQueryWithAnswer($"SELECT Region_Geo FROM Geo WHERE Id_Geo = 21;");
        string answer1 = await MyDataBase.ExecuteQueryWithAnswer($"SELECT January FROM Insolation WHERE Id_Insolation = 1;");
        //string answer = MyDataBase.ExecuteQueryWithAnswer($"SELECT AverageWindSpeed_h10 FROM WindSpeed WHERE Id_WindSpeed = 1;");
        Debug.Log(answer);
        Debug.Log(answer1);
    }
}
