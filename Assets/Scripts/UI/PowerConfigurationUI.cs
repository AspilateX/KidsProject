using System.Text;
using UnityEngine;
using System.Linq;
public class PowerConfigurationUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    private BuildingPowerConfiguration _config;
    
    public void UpdateText()
    {
        if (_config == null)
        {
            _text.text = "Конфигурация не установлена";
            return;
        }

        StringBuilder stringBuilder = new StringBuilder();

        float powerDiffence = _config.TotalAnnualProduction - _config.TotalAnnualConsumption;
        float totalPrice = _config.PowerSourcesPrice + _config.CurrentInventorPrice;
        int breakevenPoint = (int)Mathf.Round(totalPrice / (_config.TotalProductionPerHour * 3f * 24)); // 3 рубля за кВт/ч за 24 часа в день

        float battariesPrice = _config.BattariesPrice;
        float inventorPrice = _config.CurrentInventorPrice;
        float powerSourcesPrice = _config.PowerSourcesPrice;

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

        stringBuilder.Append($" мВт (<color=#388e3c>+{System.Math.Round(_config.TotalAnnualProduction * 0.001f, 2)}</color>/<color=#d32f2f>-{System.Math.Round(_config.TotalAnnualConsumption * 0.001f, 2)}</color>)");

        stringBuilder.Append($"\nТочка безубыточности: через {breakevenPoint} д.");

        stringBuilder.Append("\nИсточников питания: ");
        stringBuilder.Append(_config.PowerDevicesData.Where(x => x.Key.GetType() == typeof(PowerProductionSource)).Sum(x => x.Value.Amount));
        stringBuilder.Append(" шт.");

        stringBuilder.Append("\nИсточников потребления: ");
        stringBuilder.Append(_config.PowerDevicesData.Where(x => x.Key.GetType() == typeof(PowerConsumptionSource)).Sum(x => x.Value.Amount));
        stringBuilder.Append(" шт.");

        stringBuilder.Append($"\n\nСтоимость СП: {powerSourcesPrice} руб.");
        stringBuilder.Append($"\nСтоимость инвентора на {_config.CurrentInventorPower} Квт: {inventorPrice} руб.");
        stringBuilder.Append($"\nСтоимость аккумуляторов: {battariesPrice} руб.");
        stringBuilder.Append($"\n\n<color=#d32f2f><b>ИТОГО {powerSourcesPrice + inventorPrice + battariesPrice} руб.</b></color>");

        _text.text = stringBuilder.ToString();
    }

    public void SetConfig(BuildingPowerConfiguration config)
    {
        _config = config;
        _config.DataChanged += (PowerDevice device, PowerDeviceData data) => UpdateText();
    }

    private void OnEnable()
    {
        if (_config != null)
            _config.DataChanged += (PowerDevice device, PowerDeviceData data) => UpdateText();
    }

    private void OnDisable()
    {
        if (_config != null)
            _config.DataChanged -= (PowerDevice device, PowerDeviceData data) => UpdateText();
    }
}
