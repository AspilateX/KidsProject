using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelector : MonoBehaviour
{
    public static event Action<GameObject> Selected;

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
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, _maxCastDistance, _selectableLayers, QueryTriggerInteraction.Collide))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                    Selected?.Invoke(hit.collider.gameObject);
            }
        }
    }
}