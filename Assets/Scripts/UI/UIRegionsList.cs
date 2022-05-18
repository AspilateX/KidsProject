using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRegionsList : UIList<string> 
{
    public event Action OnRegionChanged;
    public static int CurrentRegionSunnyHours = 0;
    private int lastSelectedContainerId = 0;

    private void Awake()
    {
        CurrentRegionSunnyHours = DataBaseQueries.GetSunnyHoursOfRegion(1).GetAwaiter().GetResult();
        lastSelectedContainerId = 0;
    }

    public override void UpdateList(List<string> content)
    {
        base.UpdateList(content);
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as UINamedButtonContainer).Clicked += async (UINamedButtonContainer container) =>
            {
                int regionId = await DataBaseQueries.GetRegionIdByName(container.Content);
                CurrentRegionSunnyHours = await DataBaseQueries.GetSunnyHoursOfRegion(regionId);

                Image img = Containers[lastSelectedContainerId].gameObject.GetComponent<Image>();

                if (img != null)
                    img.color = Color.white;

                img = Containers[regionId - 1]?.gameObject.GetComponent<Image>();

                if (img != null)
                    img.color = new Color(0.203f, 0.596f, 0.858f);

                lastSelectedContainerId = regionId - 1;
                OnRegionChanged?.Invoke();
            };
    }

    private void OnDisable()
    {
        for (int i = 0; i < Containers.Count; i++)
            (Containers[i] as UINamedButtonContainer).Clicked -= async (UINamedButtonContainer container) =>
            {
                int regionId = await DataBaseQueries.GetRegionIdByName(container.Content);
                CurrentRegionSunnyHours = await DataBaseQueries.GetSunnyHoursOfRegion(regionId);

                var img = Containers[lastSelectedContainerId]?.gameObject.GetComponent<Image>();

                if (img != null)
                    img.color = Color.white;

                img = Containers[regionId - 1]?.gameObject.GetComponent<Image>();

                if (img != null)
                    img.color = Color.blue;

                lastSelectedContainerId = Containers.IndexOf(container);
                OnRegionChanged?.Invoke();
            };
    }

}