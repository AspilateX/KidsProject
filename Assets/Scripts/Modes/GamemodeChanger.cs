using UnityEngine;

public class GamemodeChanger : MonoBehaviour
{
    public static GamemodeChanger Current;
    [SerializeField]
    private CameraController _cameraController;
    [SerializeField]
    private MenuConfigurator _menuConfigurator;
    [SerializeField]
    private MenuAnimator _menuAnimator;

    private static BuildingPowerConfiguration _lastEditedConfig;
    private void OnGamemodeChanged(GamemodeType type)
    {
        Vector3 dirToBuilding = (_lastEditedConfig.transform.position - _cameraController.transform.position).normalized;

        switch (type)
        {
            case GamemodeType.PowerConfiguration:
            {
                _cameraController.CameraRotationMode = CameraRotationMode.Orbital;
                _cameraController.CameraMovementLocked = true;
                _cameraController.CameraZoomingLocked = true;

                Vector3 horizontalDir = Vector3.ProjectOnPlane(dirToBuilding, Vector3.up).normalized;

                _cameraController.LookAt(_lastEditedConfig.transform.position);
                _cameraController.MoveTo(_lastEditedConfig.transform.position + -horizontalDir * 1f + Vector3.up * 1f);

                _cameraController.SetPivotPoint(_lastEditedConfig.transform.position);
                break;
            }
            case GamemodeType.Building:
            {
                _cameraController.CameraRotationMode = CameraRotationMode.Streight;
                _cameraController.CameraMovementLocked = false;
                _cameraController.CameraZoomingLocked = false;

                _cameraController.MoveTo(_lastEditedConfig.transform.position + -dirToBuilding * 4f);
                break;
            }
        }
    }

    public void SetPowerConfigurationMode(BuildingPowerConfiguration config)
    {
        _lastEditedConfig = config;
        _menuAnimator.HideBottomMenu(0.25f, () =>
        {
            _menuConfigurator.SetPowerConfigurationUIFor(config);
            _menuAnimator.ShowBottomMenu(0.25f);
        });
        Gamemode.ChangeGamemode(GamemodeType.PowerConfiguration);
    }

    public static void SetBuildingMode()
    {
        Gamemode.ChangeGamemode(GamemodeType.Building);
    }

    private void Awake()
    {
        Current = this;
    }

    private void OnEnable()
    {
        Gamemode.OnGamemodeChanged += OnGamemodeChanged;
    }

    private void OnDisable()
    {
        Gamemode.OnGamemodeChanged -= OnGamemodeChanged;      
    }
}

