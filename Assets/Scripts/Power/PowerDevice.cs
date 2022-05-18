using UnityEngine;

[CreateAssetMenu(fileName = "new PowerDeviceData", menuName = "ScriptableObjects/PowerDeviceData", order = 1)]
public class PowerDevice : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Color CardColor;
}
