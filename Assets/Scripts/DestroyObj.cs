using UnityEngine;

public class DestroyObj : MonoBehaviour 
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
        }
    }
}
