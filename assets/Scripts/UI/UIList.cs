using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIList<T> : MonoBehaviour
{
    [SerializeField]
    private GameObject _containerPrefab;
    public List<UIContainer<T>> Containers { get; private set; } = new List<UIContainer<T>>();

    public virtual void UpdateList(List<T> content)
    {
        if (Containers.Count < content.Count)
        {
            int addCount = content.Count - Containers.Count;
            for (int i = 0; i < addCount; i++)
                Containers.Add(Instantiate(_containerPrefab, this.transform).GetComponent<UIContainer<T>>());
        }
        else if (Containers.Count > content.Count)
        {
            List<GameObject> containerGameObjects = Containers.GetRange(content.Count, Containers.Count - content.Count).Select(x => x.gameObject).ToList();
            Containers.RemoveRange(content.Count, Containers.Count - content.Count);
            containerGameObjects.ForEach(x => Destroy(x));
        }

        for (int i = 0; i < Containers.Count; i++)
            Containers[i].Initialize(content[i]);

        for (int i = 0; i < Containers.Count; i++)
            Containers[i].gameObject.SetActive(true);
    }
}