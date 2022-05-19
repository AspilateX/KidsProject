using System;

public static class BuildingEventsHolder
{
    public static event Action<Building> OnBuildingPrefabChoosen;
    public static event Action<Building> OnBuildingPlaced;
    public static event Action<Building> OnBuildingRemoving;

    public static void ChooseBuildingPrefab(Building building)
    {
        OnBuildingPrefabChoosen?.Invoke(building);
    }
    public static void PlaceBuilding(Building building)
    {
        OnBuildingPlaced?.Invoke(building);
    }
    public static void InvokeBuildingRemove(Building building)
    {
        OnBuildingRemoving?.Invoke(building);
    }
}
