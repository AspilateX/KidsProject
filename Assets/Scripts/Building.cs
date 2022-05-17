using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer MainRenderer;

    public Vector2Int Size = Vector2Int.one;
    private Camera mainCamera;
    public BuildingsGrid fly;

    private AudioSource _audioSource;

    private void Start()
    {
        mainCamera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
       // fly = GameObject.FindWithTag("Plane").GetComponent<BildingsGrid>();
    }
    public void SetTransparent(bool available)
    {
        if (available)
        { MainRenderer.material.color = Color.green; }
        else
        { MainRenderer.material.color = Color.red; }

    }
    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }

    public void Rotate(float rotation)
    {
        transform.eulerAngles = transform.up * rotation;
    }

    public void ApplyPlacement()
    {
        if (_audioSource != null)
        {
            _audioSource.pitch = Random.Range(0.8f, 1.1f);
            _audioSource.Play();
        }
    }
    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0.88f, 1, 0, 0.3f);
                Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                Gizmos.DrawCube(pos+ new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }

    }
}

[CreateAssetMenu(fileName = "new BuildingData", menuName = "ScriptableObjects/BuildingData", order = 1)]
public class BuildingData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public Building Prefab;
} 


