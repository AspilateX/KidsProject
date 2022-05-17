using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingPowerConfiguration : MonoBehaviour
{
    public Dictionary<PowerDevice, PowerDeviceData> PowerDevicesData { get; private set; } = new Dictionary<PowerDevice, PowerDeviceData>();
    public float TotalConsumption 
    { 
        get
        {
            return PowerDevicesData
                .Select(i => i.Key.Consumption * i.Value.Amount * i.Value.UsageTime)
                .Sum();
        }
    }
    public void SetPowerDevice(PowerDevice device, PowerDeviceData data)
    {
        if (!PowerDevicesData.ContainsKey(device))
            PowerDevicesData.Add(device, data);

        PowerDevicesData[device] = data;
        Debug.Log("Set " + PowerDevicesData[device].UsageTime + " to " + device.name + " in " + this.name);
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
        return new PowerDeviceData(0,device.AverageUsageTime);
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
