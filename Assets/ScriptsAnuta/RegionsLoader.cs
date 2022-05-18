using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RegionsLoader : MonoBehaviour
{
    [SerializeField]
    private UIRegionsList _uIRegionsList;
    private void Start()
    {
        UpdateItems();
    }
    public async void UpdateItems()
    {
        List<string> regions = new List<string>();

        int regionsAmount = await DataBaseQueries.GetAmountOfRegions();

        for (int i = 0; i < regionsAmount; i++)
            regions.Add(await DataBaseQueries.GetRegionName(i + 1));

        _uIRegionsList.UpdateList(regions);
    }
}

public static class DataBaseQueries
{
    public static async Task<int> GetAmountOfRegions()
    {
        return int.Parse(await MyDataBase.ExecuteQueryWithAnswer($"SELECT COUNT(*) FROM Geo;"));
    }
    public static async Task<string> GetRegionName(int regionId)
    {
        return await MyDataBase.ExecuteQueryWithAnswer($"SELECT Region_Geo FROM Geo WHERE Id_Geo = {regionId};");
    }
    public static async Task<int> GetRegionIdByName(string name)
    {
        return int.Parse(await MyDataBase.ExecuteQueryWithAnswer($"SELECT Id_Geo FROM Geo WHERE Region_Geo = '{name}';"));
    }
    public static async Task<int> GetSunnyHoursOfRegion(int regionId)
    {
        return int.Parse(await MyDataBase.ExecuteQueryWithAnswer($"SELECT Hours FROM SunAmountDays WHERE Region_SAD = {regionId};"));
    }


}
