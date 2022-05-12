using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingPowerConfiguration : MonoBehaviour
{
    public Dictionary<PowerDevice, int> PowerDevices { get; private set; } = new Dictionary<PowerDevice, int>();
    public float TotalConsumption 
    { 
        get
        {
            return PowerDevices
                .Select(i => i.Key.PowerConsumption * i.Value)
                .Sum();
        }
    }
    public void AddPowerDevice(PowerDevice device, int amount)
    {
        if (!PowerDevices.ContainsKey(device))
            PowerDevices.Add(device, 0);

        PowerDevices[device] += amount;
    }

    public void RemovePowerDevice(PowerDevice device, int amount)
    {
        if (PowerDevices.ContainsKey(device))
        {
            PowerDevices[device] -= amount;
            if (PowerDevices[device] <= 0)
                PowerDevices.Remove(device);
        }
    }

    private void Awake()
    {
        AddPowerDevice(new PowerDevice(2.5f), 3);
        AddPowerDevice(new PowerDevice(-6f), 2);
    }
}

public class PowerDevice
{
    public PowerDevice(float powerConsumption)
    {
        PowerConsumption = powerConsumption;
    }

    // ѕотребление прибора в к¬т ч (отрицательное - прибор генерирует электричество)
    public float PowerConsumption { get; private set; } = 0f;
}
