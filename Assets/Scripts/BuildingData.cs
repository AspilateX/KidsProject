using UnityEngine;

[CreateAssetMenu(fileName = "new BuildingData", menuName = "ScriptableObjects/BuildingData", order = 1)]
public class BuildingData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public Building Prefab;
} 


