using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIPowerDevicesList : UIList<PowerDevice>
{
    [SerializeField]
    private List<PowerDevice> _content = new List<PowerDevice>();

    [SerializeField]
    private bool _sortByConsumption = true;

    private BuildingPowerConfiguration _configuration;
    private void OnEnable()
    {
        if (_sortByConsumption)
           _content = _content.OrderBy(x => x.GetType().Name).Reverse().ToList();

        UpdateList(_content);

        for (int i = 0; i < Containers.Count; i++)
        {
            (Containers[i] as PowerDeviceUIContainer).DeviceDataChanged += ApplyToConfig;
        }
    }

    private void ApplyToConfig(PowerDevice device, PowerDeviceData data)
    {
        _configuration?.SetPowerDevice(device, data);
    }

    private void OnDisable()
    {
        UpdateList(new List<PowerDevice>());
        for (int i = 0; i < Containers.Count; i++)
        {
            (Containers[i] as PowerDeviceUIContainer).DeviceDataChanged -= ApplyToConfig;
        }
    }

    public void SetConfig(BuildingPowerConfiguration config)
    {
        _configuration = config;

        for (int i = 0; i < Containers.Count; i++)
        {
            PowerDeviceData data = _configuration.GetPowerDeviceData(Containers[i].Content);

            (Containers[i] as PowerDeviceUIContainer).SetAmountField(data.Amount);
            (Containers[i] as PowerDeviceUIContainer).SetHoursField(data.UsageTime);
        }
    }
}
