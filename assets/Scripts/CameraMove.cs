using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    void Update()
    {
        Vector2 inputDir = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f);
        Vector2 planarVelocity = inputDir * _speed;
        Vector3 forwardCross = Vector3.Cross(Vector3.up, transform.forward);
        Vector3 rightCross = Vector3.Cross(forwardCross, Vector3.up);

        Vector3 velocity = forwardCross * planarVelocity.x + rightCross * planarVelocity.y;

        transform.position += velocity * Time.deltaTime;
    }
}
 


