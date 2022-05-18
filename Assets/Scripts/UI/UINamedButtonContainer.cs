using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UINamedButtonContainer : UIContainer<string>
{
    public event Action<UINamedButtonContainer> Clicked;
    [SerializeField]
    private TMP_Text _name;
    [SerializeField]
    private Button _button;
    public override void Initialize(string content)
    {
        base.Initialize(content);
        _name.text = content;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => Clicked?.Invoke(this));
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => Clicked?.Invoke(this));
    }
}
