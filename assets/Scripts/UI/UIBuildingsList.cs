using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingsList : UIList<BuildingData> 
{
    public static event Action<BuildingData> BuildingChoosen;

    [SerializeField]
    private List<BuildingData> _content = new List<BuildingData>();
    private void OnEnable()
    {
        UpdateList(_content);
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as BuildingUIContainer).Clicked += InvokeChoose;
    }
    private void OnDisable()
    {
        UpdateList(_content);
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as BuildingUIContainer).Clicked -= InvokeChoose;
    }
    private void InvokeChoose(BuildingData content)
    {
        BuildingChoosen?.Invoke(content);
    }
}