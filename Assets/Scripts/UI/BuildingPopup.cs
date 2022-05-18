using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPopup : MonoBehaviour
{
    [SerializeField]
    public UIBuildingRequestsList _prefab;

    private GameObject _currentPopup;

    public static BuildingPopup Current;

    private void Awake()
    {
        Current = this;
    }

    private void OnEnable()
    {
        MouseSelector.Selected += (GameObject go) =>
        {
            if (Gamemode.CurrentGamemode == GamemodeType.Building)
            {
                if (go.TryGetComponent(out Building buildingComponent))
                    _currentPopup = Show(buildingComponent);
            }
        };

        BuildingEventsHolder.OnBuildingRemoving += (Building b) =>
        {
            Hide();
        };
        Gamemode.OnGamemodeChanged += (GamemodeType type) =>
        {
            Hide();
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                Hide();      
        }
    }
    private void OnDisable()
    {
        MouseSelector.Selected -= (GameObject go) =>
        {
            if (Gamemode.CurrentGamemode == GamemodeType.Building)
            {
                if (go.TryGetComponent(out Building buildingComponent))
                    _currentPopup = Show(buildingComponent);
            }
        };

        BuildingEventsHolder.OnBuildingRemoving -= (Building b) =>
        {
            Hide();
        };
        Gamemode.OnGamemodeChanged -= (GamemodeType type) =>
        {
            Hide();
        };
    }
    public GameObject Show(Building forBuilding)
    {
        RectTransform rectTransform;

        if (_currentPopup == null)
            rectTransform = Instantiate(_prefab, this.transform).GetComponent<RectTransform>();
        else
        {
            rectTransform = _currentPopup.GetComponent<RectTransform>();
            rectTransform.gameObject.SetActive(true);
        }

        if (rectTransform != null)
        {
            rectTransform.position = new Vector3(Input.mousePosition.x + rectTransform.sizeDelta.x / 2f, Input.mousePosition.y - rectTransform.sizeDelta.y / 2f, 0f);
            if (rectTransform.TryGetComponent(out UIBuildingRequestsList requestsList))
            {
                List<BuildingRequest> buildingRequests = new List<BuildingRequest>();

                if (forBuilding.GetComponent<BuildingPowerConfiguration>() != null)
                {
                    buildingRequests.AddRange(new List<BuildingRequest>() {                     
                        new BuildingRequest(forBuilding, BuildingRequestType.ChangeConfig),
                        new BuildingRequest(forBuilding, BuildingRequestType.CopyConfig),
                        new BuildingRequest(forBuilding, BuildingRequestType.PasteConfig)}
                    );
                }
                buildingRequests.Add(new BuildingRequest(forBuilding, BuildingRequestType.RemoveBuilding));

                requestsList.Instantiate(buildingRequests);
                return rectTransform.gameObject;
            }
            rectTransform.gameObject.SetActive(false);
        }
        return null;
    }
    public void Hide()
    {
        if (_currentPopup != null)
            _currentPopup.SetActive(false);
    }
}
