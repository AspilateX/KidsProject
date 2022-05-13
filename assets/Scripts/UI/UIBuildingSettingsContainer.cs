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
                    _button?.onClick.AddListener(RequestPowerConfigurationMode);
                    break;
                }
            case BuildingRequestType.CopyConfig: break;
            case BuildingRequestType.PasteConfig: break;
            case BuildingRequestType.RemoveBuilding:
                {
                    _button?.onClick.AddListener(RequestDelete);
                    break;
                }
            default: break;
        }
    }

    private void RequestPowerConfigurationMode()
    {
        if (Content.Target.TryGetComponent(out BuildingPowerConfiguration config))
            GamemodeChanger.SetPowerConfigurationMode(config);
    }
    private void RequestCopyConfiguration()
    {

    }
    private void RequestPasteConfiguration()
    {

    }
    private void RequestDelete()
    {
        BuildingEventsHolder.InvokeBuildingRemove(Content.Target);
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

public static class BuildingEventsHolder
{
    public static event Action<Building> OnBuildingPrefabChoosen;
    public static event Action<Building> OnBuildingRemoving;

    public static void ChooseBuildingPrefab(Building building)
    {
        OnBuildingPrefabChoosen?.Invoke(building);
    }
    public static void InvokeBuildingRemove(Building building)
    {
        OnBuildingRemoving?.Invoke(building);
    }
}
