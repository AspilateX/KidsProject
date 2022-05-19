using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class UIBuildingSettingsContainer : UIContainer<BuildingRequest>
{
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private Button _button;
    public override void Initialize(BuildingRequest content)
    {
        base.Initialize(content);

        if (_button == null)
            return;

        string name = "";

        switch(content.RequestType)
        {
            case BuildingRequestType.ChangeConfig: name = "Изменить конфигурацию"; break;
            case BuildingRequestType.CopyConfig: name = "Скопировать конфигурацию"; break;
            case BuildingRequestType.PasteConfig: name = "Вставить конфигурацию"; break;
            case BuildingRequestType.RemoveBuilding: name = "Удалить"; break;
            default: name = "Invalid request type"; break;
        }
        _name.text = name;

        switch (content.RequestType)
        {
            case BuildingRequestType.ChangeConfig:
                {
                    _button.onClick.RemoveAllListeners();
                    _button.onClick.AddListener(RequestPowerConfigurationMode);
                    break;
                }
            case BuildingRequestType.CopyConfig:
                {
                    _button.onClick.RemoveAllListeners();
                    _button.onClick.AddListener(RequestCopyConfiguration);
                    break;
                } 
            case BuildingRequestType.PasteConfig:
                {
                    _button.onClick.RemoveAllListeners();
                    if (BuildingConfigurationBuffer.Buffer == null)
                        _button.enabled = false;
                    else
                    {
                        _button.enabled = true;
                        _button.onClick.AddListener(RequestPasteConfiguration);
                    }
                    break;
                }
            case BuildingRequestType.RemoveBuilding:
                {
                    _button.onClick.RemoveAllListeners();
                    _button.onClick.AddListener(RequestDelete);
                    break;
                }
            default: break;
        }
    }

    private void RequestPowerConfigurationMode()
    {
        if (Content.Target.TryGetComponent(out BuildingPowerConfiguration config))
            GamemodeChanger.Current.SetPowerConfigurationMode(config);

        BuildingPopup.Current.Hide();
    }
    private void RequestCopyConfiguration()
    {
        if (Content.Target.TryGetComponent(out BuildingPowerConfiguration config))
            BuildingConfigurationBuffer.Buffer = config;

        BuildingPopup.Current.Hide();
    }
    private void RequestPasteConfiguration()
    {
        if (Content.Target.TryGetComponent(out BuildingPowerConfiguration config))
            config.SetPowerDevicesData(BuildingConfigurationBuffer.Buffer.PowerDevicesData);

        BuildingPopup.Current.Hide();

    }
    private void RequestDelete()
    {
        BuildingEventsHolder.InvokeBuildingRemove(Content.Target);
        BuildingPopup.Current.Hide();
    }
}

public class BuildingRequest
{
    public BuildingRequest(Building target, BuildingRequestType requestType)
    {
        Target = target;
        RequestType = requestType;
    }
    public Building Target { get; }
    public BuildingRequestType RequestType { get; }
}
public enum BuildingRequestType
{
    ChangeConfig,
    CopyConfig,
    PasteConfig,
    RemoveBuilding
}
