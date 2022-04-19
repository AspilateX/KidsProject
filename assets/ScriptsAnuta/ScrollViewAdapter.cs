using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewAdapter : MonoBehaviour
{
    public RectTransform prefab;
    public RectTransform content;
    public GameObject con;
    public Text chose;
    private int region;
    public static float insolation = 0.0f;
   

    public void UpdateItems()
    {
        int modelsCount = 85;
        con.SetActive(true);
        StartCoroutine(GetItems(modelsCount, results => OnRecievedModels(results)));
    }

    void OnRecievedModels(TestItemModel[] models)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
        }
    }

    void InitializeItemView(GameObject viewGameObject, TestItemModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.titleText.text = model.title;
        view.clickButton.GetComponentInChildren<Text>().text = model.buttonText;
        view.clickButton.onClick.AddListener(
            () =>
            {
                Debug.Log(view.titleText.text + " is clicked!");
                region = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT Id_Geo FROM Geo WHERE Region_Geo = '{view.titleText.text}';"));
                insolation = float.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT AnnualAverage FROM Insolation WHERE Id_Insolation = '{region}';"));
                Debug.Log(insolation);
                chose.text = view.titleText.text;
                con.SetActive(false);
            }
            );
    }

    IEnumerator GetItems (int count, System.Action<TestItemModel[]> callback)
    {
        yield return new WaitForSeconds(0.2f);
        var results = new TestItemModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new TestItemModel();
            results[i].title = MyDataBase.ExecuteQueryWithAnswer($"SELECT Region_Geo FROM Geo WHERE Id_Geo = {(i+1)};");
            results[i].buttonText = "Select";
        }
        callback(results);
    }

    public class TestItemView
    {
        public Text titleText;
        public Button clickButton;
        public TestItemView(Transform rootView)
        {
            titleText = rootView.Find("TitleText").GetComponent<Text>();
            clickButton = rootView.Find("ClickButton").GetComponent<Button>();
        }
    }

    public class TestItemModel
    {
        public string title;
        public string buttonText;       
    }
}
