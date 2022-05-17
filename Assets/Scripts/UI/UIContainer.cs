using UnityEngine;

public class UIContainer<T> : MonoBehaviour
{
    public T Content { get; private set; }
    public virtual void Initialize(T content)
    {
        Content = content;
    }
}