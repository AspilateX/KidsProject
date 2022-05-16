using UnityEngine;

public class MenuConfigurator : MonoBehaviour
{
    [SerializeField]
    private UIBuildingsList _buildingsList;
    [SerializeField]
    private UIPowerDevicesList _powerDevicesList;

    private void Start()
    {
        SetBuildingUI();
    }

    public void SetPowerConfigurationUIFor(BuildingPowerConfiguration config)
    {
        _buildingsList.enabled = false;
        _powerDevicesList.enabled = true;
        _powerDevicesList.SetConfig(config);
    }
    public void SetBuildingUI()
    {
        _buildingsList.enabled = true;
        _powerDevicesList.enabled = false;
    }
}
