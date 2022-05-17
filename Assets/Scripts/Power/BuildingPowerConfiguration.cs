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
                .Select(i => i.Key.Consumption * i.Value)
                .Sum();
        }
    }
    public void AddPowerDevice(PowerDevice device, int amount)
    {
        if (!PowerDevices.ContainsKey(device))
            PowerDevices.Add(device, 0);

        PowerDevices[device] = amount;
        Debug.Log("Set " + PowerDevices[device] + " to " + device.name + " in " + this.name);
    }

    public void RemovePowerDevice(PowerDevice device)
    {
        if (PowerDevices.ContainsKey(device))
        {
            PowerDevices.Remove(device);
        }
    }
}
