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

        stringBuilder.Append($"Солнечных часов за год в выбранном регионе: <color=#f39c12><b>{UIRegionsList.CurrentRegionSunnyHours}</b></color>");

        if (powerDiffence > 0)
        {
            stringBuilder.Append("\nГодовая выработка: ");
            stringBuilder.Append("<color=#388e3c><b>");
            stringBuilder.Append("+");
            stringBuilder.Append(System.Math.Round(Mathf.Abs(powerDiffence * 0.001f), 2));
            stringBuilder.Append("</b></color>");
        }
        else
        {
            stringBuilder.Append("\nГодовое потребление: ");
            stringBuilder.Append("<color=#d32f2f><b>");
            stringBuilder.Append(System.Math.Round(powerDiffence * 0.001f, 2));
            stringBuilder.Append("</b></color>");
        }

        stringBuilder.Append($" мВт (<color=#388e3c>+{System.Math.Round(totalAnnualProduction * 0.001f, 2)}</color>/<color=#d32f2f>-{System.Math.Round(totalAnnualConsumption * 0.001f, 2)}</color>)");

        stringBuilder.Append($"\n\n<color=#d32f2f><b>ИТОГО (НА ВСЕ ДОМА) {totalPrice} руб.</b></color>");

        _text.text = stringBuilder.ToString();
    }

    private void OnEnable()
    {
        for (int i =0; i < _configs.Count; i++)
            _configs[i].DataChanged += (PowerDevice device, PowerDeviceData data) => UpdateText();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _configs.Count; i++)
            _configs[i].DataChanged -= (PowerDevice device, PowerDeviceData data) => UpdateText();
    }
}