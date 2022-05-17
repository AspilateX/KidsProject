using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;

public class PowerDeviceUIContainer : UIContainer<PowerDevice>
{
    public event Action<PowerDevice, PowerDeviceData> DeviceDataChanged;
    [SerializeField]
    private TMP_InputField _devicesAmountInputField;
    [SerializeField]
    private TMP_InputField _devicesHoursInputField;
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backgroundImage;

    public override void Initialize(PowerDevice content)
    {
        base.Initialize(content);
        _nameText.text = content.Name;
        _iconImage.sprite = content.Icon;
        _backgroundImage.color = content.CardColor;
        
    }

    public void SetAmountField(int newValue)
    {
        _devicesAmountInputField.text = newValue.ToString() + " шт.";
    }
    public void SetHoursField(float newValue)
    {
        _devicesHoursInputField.text = newValue.ToString() + " ч.";
    }

    public void AddAmount(int amount)
    {
        int result = Math.Max(0, GetValidatedAmount(_devicesAmountInputField.text) + amount);
        SetAmountField(result);
        ApplyFields();
    }

    public void AddHours(float hours)
    {
        float result = Math.Max(0, GetValidatedHours(_devicesHoursInputField.text) + hours);
        SetHoursField(result);
        ApplyFields();
    }

    private int GetValidatedAmount(string input)
    {
        input = string.Concat(input.Where(char.IsDigit));
        if (int.TryParse(input, out int result))
            return Mathf.Max(0, result);
        return 0;
    }
    private float GetValidatedHours(string input)
    {
        input = Regex.Replace(input, "\\.? ч.", "");
        if (float.TryParse(input, out float result))
            return Mathf.Max(0, result);
        return 0;
    }

    private void ApplyFields()
    {
        int amount = GetValidatedAmount(_devicesAmountInputField.text);
        float usageTime = GetValidatedHours(_devicesHoursInputField.text);
        SetAmountField(amount);
        SetHoursField(usageTime);
        PowerDeviceData data = new PowerDeviceData(amount, usageTime);

        DeviceDataChanged?.Invoke(Content, data);
    }

    private void OnEnable()
    {
        _devicesAmountInputField.onEndEdit.AddListener((string text) => ApplyFields());
        _devicesAmountInputField.onSubmit.AddListener((string text) => ApplyFields());
    }

    private void OnDisable()
    {
        _devicesAmountInputField.onEndEdit.RemoveListener((string text) => ApplyFields());
        _devicesAmountInputField.onSubmit.RemoveListener((string text) => ApplyFields());
    }

}
