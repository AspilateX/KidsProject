using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GlobalConfigurationUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    private List<BuildingPowerConfiguration> _configs = new List<BuildingPowerConfiguration>();

    public void UpdateText()
    {
        _text.alignment = TMPro.TextAlignmentOptions.Center;
        StringBuilder stringBuilder = new StringBuilder();

        float powerDiffence = 0f;
        float totalPrice = 0f;
        float totalAnnualProduction = 0f;
        float totalAnnualConsumption = 0f;

        for (int i = 0; i < _configs.Count; i++)
        {
            powerDiffence += _configs[i].TotalAnnualProduction - _configs[i].TotalAnnualConsumption;
            totalPrice += _configs[i].PowerSourcesPrice + _configs[i].CurrentInventorPrice + _configs[i].BattariesPrice;
            totalAnnualProduction += _configs[i].TotalAnnualProduction;
            totalAnnualConsumption += _configs[i].TotalAnnualConsumption;
        }

        stringBuilder.Append($"Солнечных часов за год в выбранном регионе: \n\n<size=+8><color=#f39c12><b>{UIRegionsList.CurrentRegionSunnyHours}</b></color></size>\n");

        if (powerDiffence > 0)
        {
            stringBuilder.Append($"\nГодовая <color=#388e3c><b>выработка</b></color> всего поселения: \n\n<size=+8><color=#388e3c><b>+{System.Math.Round(Mathf.Abs(powerDiffence * 0.001f), 2)}</b></color></size>");
        }
        else
        {
            stringBuilder.Append($"\nГодовое <color=#d32f2f><b>потребление</b></color> всего поселения: \n\n<size=+8><color=#d32f2f><b>{System.Math.Round(powerDiffence * 0.001f, 2)}</b></color></size>");
        }

        stringBuilder.Append($" мВт (<color=#388e3c>+{System.Math.Round(totalAnnualProduction * 0.001f, 2)}</color>/<color=#d32f2f>-{System.Math.Round(totalAnnualConsumption * 0.001f, 2)}</color>)");

        stringBuilder.Append($"\n\n<size=+2><color=#d32f2f><b>Стоимость проекта {System.Math.Round(totalPrice, 2)} руб.</b></color></size>");

        _text.text = stringBuilder.ToString();
    }

    public void Add(BuildingPowerConfiguration config)
    {
        _configs.Add(config);
        config.DataChanged += (PowerDevice device, PowerDeviceData data) => UpdateText();
    }

    public void Remove(BuildingPowerConfiguration config)
    {
        config.DataChanged -= (PowerDevice device, PowerDeviceData data) => UpdateText();
        _configs.Remove(config);
    }

    private void OnEnable()
    {
        BuildingConfigurationBuffer.OnBufferChanged += (BuildingPowerConfiguration buffer) =>
        {
            UpdateText();
        };
        BuildingsGrid.OnBuildingPlaced += (Building building) =>
        {
            if(building.TryGetComponent(out BuildingPowerConfiguration config))
            {
                Add(config);
                UpdateText();
            }
        };
        BuildingsGrid.OnBuildingRemoved += (Building building) =>
        {
            if (building.TryGetComponent(out BuildingPowerConfiguration config))
            {
                Remove(config);
                UpdateText();
            }
        };
        for (int i =0; i < _configs.Count; i++)
            _configs[i].DataChanged += (PowerDevice device, PowerDeviceData data) => UpdateText();
    }

    private void OnDisable()
    {
        BuildingConfigurationBuffer.OnBufferChanged -= (BuildingPowerConfiguration buffer) =>
        {
            UpdateText();
        };
        BuildingsGrid.OnBuildingPlaced -= (Building building) =>
        {
            if (building.TryGetComponent(out BuildingPowerConfiguration config))
            {
                Add(config);
            }
        };
        BuildingsGrid.OnBuildingRemoved -= (Building building) =>
        {
            if (building.TryGetComponent(out BuildingPowerConfiguration config))
            {
                Remove(config);
            }
        };
        for (int i = 0; i < _configs.Count; i++)
            _configs[i].DataChanged -= (PowerDevice device, PowerDeviceData data) => UpdateText();
    }
}