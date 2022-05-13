using UnityEngine;
public class BuildingPopupFactory : MonoBehaviour
{
    [SerializeField]
    public UIBuildingRequestsList _prefab;

    private GameObject _currentPopup;

    private void OnEnable()
    {
        MouseSelector.Selected += (GameObject go) =>
        {
            if (Gamemode.CurrentGamemode == GamemodeType.Building)
            {
                if (go.TryGetComponent(out Building buildingComponent))
                    _currentPopup = Create(buildingComponent);
            }
        };

        BuildingsGrid.OnBuildingRemoved += () =>
        {
            Remove();
        };
    }
    private void OnDisable()
    {
        MouseSelector.Selected -= (GameObject go) =>
        {
            if (Gamemode.CurrentGamemode == GamemodeType.Building)
            {
                if (go.TryGetComponent(out Building buildingComponent))
                    _currentPopup = Create(buildingComponent);
            }
        };
        BuildingsGrid.OnBuildingRemoved -= () =>
        {
            Remove();
        };
    }
    public GameObject Create(Building building)
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
            if(rectTransform.TryGetComponent(out UIBuildingRequestsList requestsList))
            {
                requestsList.Instantiate(building);
                return rectTransform.gameObject;
            }
            rectTransform.gameObject.SetActive(false);
        }
        return null;
    }
    public void Remove()
    {
        if (_currentPopup != null)
            _currentPopup.SetActive(false);
    }
}
