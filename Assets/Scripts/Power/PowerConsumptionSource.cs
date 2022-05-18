using UnityEngine;

[CreateAssetMenu(fileName = "new PowerConsumptionSource", menuName = "ScriptableObjects/PowerConsumptionSource", order = 1)]
public class PowerConsumptionSource : PowerDevice
{
    public float Consumption;
    public float AverageUsageTime = 1;
}
