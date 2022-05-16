using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;

public class PowerDeviceUIContainer : UIContainer<PowerDevice>
{
    public event Action<PowerDevice, int> ValueChanged;
    [SerializeField]
    private TMP_InputField _devicesAmountInputField;
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private Image _iconImage;

    public override void Initialize(PowerDevice content)
    {
        base.Initialize(content);
        _devicesAmountInputField.text = "0";
        _nameText.text = content.Name;
        _iconImage.sprite = content.Icon;
    }

    public void SetText(int newValue)
    {
        _devicesAmountInputField.text = newValue.ToString() + " шт.";
    }

    private int GetValidatedInput(string input)
    {
        input = string.Concat(input.Where(char.IsDigit));
        if (int.TryParse(input, out int result))
            return result;
        return 0;
    }

    private void ApplyInput(string input)
    {
        int result = Mathf.Max(0, GetValidatedInput(input));
        SetText(result);
        ValueChanged?.Invoke(Content, result);
    }

    private void OnEnable()
    {
        _devicesAmountInputField.onEndEdit.AddListener((string text) => ApplyInput(text));
    }

    private void OnDisable()
    {
        _devicesAmountInputField.onEndEdit.RemoveListener((string text) => ApplyInput(text));
    }

}
