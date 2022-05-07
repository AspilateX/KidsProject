using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewAdapter : MonoBehaviour
{
    public RectTransform prefab;
    public RectTransform content;
    public GameObject con;
    public Text chose;
    private int region;
    public static float insolation = 0.0f;

    [Space]
    [SerializeField]
    private UIDocument UIDocument;

    private VisualElement _UIRoot;
    private DropdownField _dropDownField;
    private void Awake()
    {
        _UIRoot = UIDocument.rootVisualElement;
        _dropDownField = _UIRoot.Q<DropdownField>("TopDropdown");
    }
    private void Start()
    {
        UpdateItems();
    }
    public void UpdateItems()
    {
        int modelsCount = 85;
        con.SetActive(true);
        GetItems(modelsCount, results => OnRecievedModels(results));
    }

    void OnRecievedModels(TestItemModel[] models)
    {
        _dropDownField.choices = models.Select(x => x.title).ToList();
    }

    async void InitializeItemView(GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.titleText.text = model.title;
        view.clickButton.GetComponentInChildren<Text>().text = model.buttonText;
        view.clickButton.onClick.AddListener(
            async () =>
            {
                Debug.Log(view.titleText.text + " is clicked!");
                region = int.Parse(await MyDataBase.ExecuteQueryWithAnswer($"SELECT Id_Geo FROM Geo WHERE Region_Geo = '{view.titleText.text}';"));
                insolation = float.Parse(await MyDataBase.ExecuteQueryWithAnswer($"SELECT AnnualAverage FROM Insolation WHERE Id_Insolation = '{region}';"));
                Debug.Log(insolation);
                chose.text = view.titleText.text;
                con.SetActive(false);
            }
            );
    }

    async void GetItems (int count, System.Action<TestItemModel[]> callback)
    {
        var results = new TestItemModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new TestItemModel();
            results[i].title = await MyDataBase.ExecuteQueryWithAnswer($"SELECT Region_Geo FROM Geo WHERE Id_Geo = {(i+1)};");
            results[i].buttonText = "Select";
        }
        callback(results);
    }

    public class TestItemView
    {
        public Text titleText;
        public UnityEngine.UI.Button clickButton;
        public TestItemView(Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<UnityEngine.UI.Button>();
        }
    }

    public class TestItemModel
    {
        public string title;
        public string buttonText;       
    }
}
