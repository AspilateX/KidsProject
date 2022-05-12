using UnityEngine;

public class GamemodeSetup : MonoBehaviour
{
    [SerializeField]
    private CameraController _cameraController;

    private void OnGamemodeChanged(GamemodeType type)
    {
        switch (type)
        {
            case GamemodeType.PowerConfiguration:
            {
                _cameraController.CameraRotationMode = CameraRotationMode.Orbital;
                _cameraController.SetPivotPoint(_cameraController.transform.position + _cameraController.transform.forward * 5f);
                _cameraController.CameraMovementLocked = true;
                _cameraController.CameraZoomingLocked = true;
                break;
            }
            case GamemodeType.Building:
            {
                _cameraController.CameraRotationMode = CameraRotationMode.Streight;
                _cameraController.CameraMovementLocked = false;
                _cameraController.CameraZoomingLocked = false;
                break;
            }
        }
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

