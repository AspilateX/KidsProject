using System;
using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    public static event Action<BuildingPowerConfiguration> PowerConfigurationSelected;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LayerMask _selectableLayers;
    [SerializeField]
    private float _maxCastDistance = 100f;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, _maxCastDistance, _selectableLayers, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.TryGetComponent(out BuildingPowerConfiguration PowerSettingsComponent))
                {
                    PowerConfigurationSelected?.Invoke(PowerSettingsComponent);
                    return;
                }
            }
        }
    }

    private void OnEnable()
    {
        PowerConfigurationSelected += (BuildingPowerConfiguration config) => Gamemode.ChangeGamemode(GamemodeType.PowerConfiguration);
    }
}