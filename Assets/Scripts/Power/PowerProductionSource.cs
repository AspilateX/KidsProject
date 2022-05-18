using UnityEngine;

[CreateAssetMenu(fileName = "new PowerProductionSource", menuName = "ScriptableObjects/PowerProductionSource", order = 2)]

public class PowerProductionSource : PowerDevice
{
    public float Price;
    public float Production;
}