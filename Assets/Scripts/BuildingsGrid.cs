using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class BuildingsGrid : MonoBehaviour
{
    public static event Action OnBuildingRemoved;
    public Vector2Int GridSize = new Vector2Int (100,100);
    [SerializeField]
    private ParticleSystem _onPlacePS;

    [SerializeField]
    private UIBuildingsList _buildingsList;

    private Building[,] grid;
   // public bool [,] gridControl;

    public Building CurrentBuildingInstance { get; private set; }
    
    private Camera mainCamera;

    private Building _lastPrefab;
    private float _currentRotation = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        //gridControl = GameObject.FindObjectOfType<GridControl>().worldGrid;
        /*foreach(bool elem in gridControl)
        {
            Debug.Log(gridControl);
        }*/
        
        grid = new Building[GridSize.x, GridSize.y];
        mainCamera = Camera.main;

        if (_onPlacePS != null)
            _onPlacePS = Instantiate(_onPlacePS, this.transform);
    }

    private void OnEnable()
    {
        BuildingEventsHolder.OnBuildingPrefabChoosen += PlacingBuilding;
        BuildingEventsHolder.OnBuildingRemoving += RemoveBuilding;
    }
    private void OnDisable()
    {
        BuildingEventsHolder.OnBuildingPrefabChoosen -= PlacingBuilding;
        BuildingEventsHolder.OnBuildingRemoving -= RemoveBuilding;
    }
    public void PlacingBuilding(Building buildPrefab)
    {
        _lastPrefab = buildPrefab;
        if (CurrentBuildingInstance != null)
            Destroy(CurrentBuildingInstance.gameObject);

        CurrentBuildingInstance = Instantiate(buildPrefab);
        CurrentBuildingInstance.gameObject.layer = LayerMask.NameToLayer("Default");
        RotateBuilding();
    }

    private void Update()
    {
       SetGridPosition(CurrentBuildingInstance);
    }

    public void SetGridPosition(Building building)
    {
        if (building != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;
                if (x < 0 || x > GridSize.x - building.Size.x) available = false;
                if (y < 0 || y > GridSize.y - building.Size.y) available = false;

                
                if (available && IsPlaceTaken(x, y)) available = false;

                building.transform.position = new Vector3(x, building.transform.position.y, y);
                building.SetTransparent(available);


                if (available)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        _currentRotation += 90f;
                        RotateBuilding();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!EventSystem.current.IsPointerOverGameObject())
                            ApplyBuildingAtPosition(x, y);
                    }
                }
            }
        }
    }


    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < 1; y++)
            {
               if (grid[placeX + x, placeY + y] !=null) return true;
                //if (gridControl[placeX + x, placeY + y] != false) return true;
            }
        }
        return false;
    }
    private void ApplyBuildingAtPosition(int placeX, int placeY)
    {
        for (int x=0; x< CurrentBuildingInstance.Size.x; x++)
        {
        for (int y = 0; y< CurrentBuildingInstance.Size.y; y++)
            {

               grid[placeX + x, placeY + y] = CurrentBuildingInstance;
                //gridControl[placeX + x, placeY + y]= true;
               
            }
        }

        if (_onPlacePS != null)
        {
            _onPlacePS.transform.position = CurrentBuildingInstance.transform.position;
            _onPlacePS.Play();
        }

        CurrentBuildingInstance.SetNormal();
        CurrentBuildingInstance.ApplyPlacement();
        CurrentBuildingInstance.gameObject.layer = LayerMask.NameToLayer("Selectable");
        CurrentBuildingInstance = null;

        if (_lastPrefab != null)
            PlacingBuilding(_lastPrefab);

    }

    private void RotateBuilding()
    {
        CurrentBuildingInstance.Rotate(_currentRotation);
    }

    public void RemoveBuilding(Building building)
    {
        if (building == null) 
            return;

        Destroy(building.gameObject);
        OnBuildingRemoved?.Invoke();
    }
}
