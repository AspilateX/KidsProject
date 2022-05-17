using System;
using System.Collections.Generic;
using UnityEngine;

public class UIPowerDevicesList : UIList<PowerDevice>
{
    [SerializeField]
    private List<PowerDevice> _content = new List<PowerDevice>();

    private BuildingPowerConfiguration _configuration;
    private void OnEnable()
    {
        UpdateList(_content);
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as PowerDeviceUIContainer).ValueChanged += ApplyToConfig;
    }

    private void ApplyToConfig(PowerDevice device, int amount)
    {
        _configuration?.AddPowerDevice(device, amount);
    }

    private void OnDisable()
    {
        UpdateList(new List<PowerDevice>());
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as PowerDeviceUIContainer).ValueChanged -= ApplyToConfig;
    }

    public void SetConfig(BuildingPowerConfiguration config)
    {
        _configuration = config;
    }
}
