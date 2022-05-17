using UnityEngine;

public class MenuConfigurator : MonoBehaviour
{
    [SerializeField]
    private UIBuildingsList _buildingsList;
    [SerializeField]
    private UIPowerDevicesList _powerDevicesList;
    [SerializeField]
    private TMPro.TMP_Text _centerHeaderText;
    [SerializeField]
    private TMPro.TMP_Text _rightHeaderText;
    [SerializeField]
    private GameObject _rightBuildingUI;
    [SerializeField]
    private GameObject _rightPowerConfigurationUI;
    [SerializeField]
    private GameObject _powerConfigurationExitButton;

    private const string PowerConfigurationCenterHeader = "Электроприборы и источники питания";
    private const string BuildingCenterHeader = "Здания и декорации";
    private const string PowerConfigurationRightHeader = "Общая информация";
    private const string BuildingRightHeader = "Регионы";

    private void Start()
    {
        SetBuildingUI();
    }

    public void SetPowerConfigurationUIFor(BuildingPowerConfiguration config)
    {
        _buildingsList.enabled = false;
        _powerDevicesList.enabled = true;

        _rightBuildingUI.SetActive(false);
        _rightPowerConfigurationUI.SetActive(true);

        _centerHeaderText.text = PowerConfigurationCenterHeader;
        _rightHeaderText.text = PowerConfigurationRightHeader;

        _powerConfigurationExitButton.SetActive(true);

        _powerDevicesList.SetConfig(config);
    }
    public void SetBuildingUI()
    {
        _buildingsList.enabled = true;
        _powerDevicesList.enabled = false;

        _rightBuildingUI.SetActive(true);
        _rightPowerConfigurationUI.SetActive(false);

        _centerHeaderText.text = BuildingCenterHeader;
        _rightHeaderText.text = BuildingRightHeader;

        _powerConfigurationExitButton.SetActive(false);
    }
}
