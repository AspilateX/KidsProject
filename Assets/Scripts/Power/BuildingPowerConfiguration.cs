using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingPowerConfiguration : MonoBehaviour
{
    public event Action<PowerDevice, PowerDeviceData> DataChanged;
    public Dictionary<PowerDevice, PowerDeviceData> PowerDevicesData { get; set; } = new Dictionary<PowerDevice, PowerDeviceData>();

    public float CurrentInventorPower 
    { 
        get
        {
            return _inventorsPrices.FirstOrDefault(x => x.Key >= TotalProductionPerHour).Key;
        } 
    }
    public float CurrentInventorPrice 
    { 
        get
        {
            return _inventorsPrices.FirstOrDefault(x => x.Key >= TotalProductionPerHour).Value;
        }
    }
    
    public float BattariesPrice
    {
        get
        {
            return Mathf.Ceil(TotalProductionPerHour / _battaryCapacity) * _battaryPrice;
        }
    }

    private Dictionary<float, float> _inventorsPrices = new Dictionary<float, float>()
    {
        { 0f, 0f },
        { 0.6f, 9600f },
        { 1.5f, 26900f },
        { 3f, 50600f },
        { 6f, 84000f },
        { 10f, 158900f },
        { 15f, 278800f },
        { 18f, 278800f + 50600f},
        { 21f, 278800f + 84000f },
        { 25f, 278800f + 158900f },
        { 30f, 278800f * 2f },
        { 40f, 278800f * 2f + 158900f },
        { 45f, 278800f * 3f},
        { 60f, 278800f * 4f }
    };
    private float _battaryCapacity = 1.68f; // јккумул€тор емкостью 140ј на 12¬ = 1.68 к¬т
    private float _battaryPrice = 8300f; // —редн€€ цена
    public float TotalConsumptionPerDay // ¬ к¬т/д
    { 
        get
        {
            var sources = PowerDevicesData.Where(i => i.Key.GetType() == typeof(PowerConsumptionSource));
            float result = 0f;
            foreach (var source in sources)
                result += (source.Key as PowerConsumptionSource).Consumption * source.Value.UsageTime * source.Value.Amount;
            return result;
        }
    }
    public float TotalConsumptionPerHour // ¬ к¬т/ч
    {
        get
        {
            var sources = PowerDevicesData.Where(i => i.Key.GetType() == typeof(PowerConsumptionSource));
            float result = 0f;
            foreach (var source in sources)
                result += (source.Key as PowerConsumptionSource).Consumption * source.Value.Amount;
            return result;
        }
    }
    public float TotalProductionPerHour // ¬ к¬т/ч
    {
        get
        {
            return PowerDevicesData.Where(i => i.Key.GetType() == typeof(PowerProductionSource)).Sum(x => (x.Key as PowerProductionSource).Production * x.Value.Amount);
        }
    }
    public float TotalAnnualProduction 
    { 
        get
        {
            return Mathf.Abs(TotalProductionPerHour) * UIRegionsList.CurrentRegionSunnyHours;
        } 
    }
    public float TotalAnnualConsumption
    {
        get
        {
            return Mathf.Abs(TotalConsumptionPerDay) * 365;
        }
    }
    public float PowerSourcesPrice
    {
        get
        {
            return PowerDevicesData.Where(i => i.Key.GetType() == typeof(PowerProductionSource)).Sum(x => (x.Key as PowerProductionSource).Price * x.Value.Amount);
        }
    }
    public void SetPowerDevice(PowerDevice device, PowerDeviceData data)
    {
        if (!PowerDevicesData.ContainsKey(device))
            PowerDevicesData.Add(device, data);

        PowerDevicesData[device] = data;

        DataChanged?.Invoke(device, data);
    }

    public void RemovePowerDevice(PowerDevice device)
    {
        if (PowerDevicesData.ContainsKey(device))
        {
            PowerDevicesData.Remove(device);
        }
    }

    public PowerDeviceData GetPowerDeviceData(PowerDevice device)
    {
        if (PowerDevicesData.ContainsKey(device))
            return PowerDevicesData[device];

        if (device is PowerConsumptionSource)
            return new PowerDeviceData(0, (device as PowerConsumptionSource).AverageUsageTime);

        return new PowerDeviceData(0,-1);
    }
}

public struct PowerDeviceData
{
    public int Amount;
    public float UsageTime;

    public PowerDeviceData(int amount, float usageTime)
    {
        Amount = amount;
        UsageTime = usageTime;
    }
}
