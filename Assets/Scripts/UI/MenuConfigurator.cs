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
    private GameObject _powerConfigurationExitButton;
    [SerializeField]
    private PowerConfigurationUI _powerConfigurationUI;
    [SerializeField]
    private GlobalConfigurationUI _globalConfigurationUI;
    [SerializeField]
    private UIRegionsList _regionsList;

    private const string PowerConfigurationCenterHeader = "Электроприборы и источники питания";
    private const string BuildingCenterHeader = "Здания и декорации";

    private void Start()
    {
        SetBuildingUI();
    }

    public void SetPowerConfigurationUIFor(BuildingPowerConfiguration config)
    {
        _buildingsList.enabled = false;
        _powerDevicesList.enabled = true;

        _centerHeaderText.text = PowerConfigurationCenterHeader;

        _powerConfigurationExitButton.SetActive(true);

        _globalConfigurationUI.enabled = false;
        _powerConfigurationUI.enabled = true;
        _powerDevicesList.SetConfig(config);
        _powerConfigurationUI.SetConfig(config);
        _powerConfigurationUI.UpdateText();

        _regionsList.OnRegionChanged += () => _powerConfigurationUI.UpdateText();
        _regionsList.OnRegionChanged -= () => _globalConfigurationUI.UpdateText();
    }
    public void SetBuildingUI()
    {
        _buildingsList.enabled = true;
        _powerDevicesList.enabled = false;

        _centerHeaderText.text = BuildingCenterHeader;

        _powerConfigurationExitButton.SetActive(false);
        
        _powerConfigurationUI.enabled = false;
        _globalConfigurationUI.enabled = true;
        _globalConfigurationUI.UpdateText();

        _regionsList.OnRegionChanged += () => _globalConfigurationUI.UpdateText();
        _regionsList.OnRegionChanged -= () => _powerConfigurationUI.UpdateText();
    }
}
