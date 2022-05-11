using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingUIContainer : UIContainer<BuildingData>
{
    public event Action<BuildingData> Clicked;

    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private TMP_Text _nameTMP;
    [SerializeField]
    private Button _button;
    public override void Initialize(BuildingData content)
    {
        base.Initialize(content);
        _nameTMP.text = content.Name;
        _backgroundImage.sprite = content.Icon;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => Clicked?.Invoke(Content));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => Clicked?.Invoke(Content));
    }
}
