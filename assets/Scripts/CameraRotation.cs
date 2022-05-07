using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private float minAltitude = 1f;
    [SerializeField]
    private float maxAltitude = 4f;
    [SerializeField]
    private float scrollSensetivity = 5f;
    [SerializeField]
    private float rotationSensetivity = 3f;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minAltitude || 
            Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxAltitude)
        {
            Vector3 newPosition = transform.position + Input.GetAxis("Mouse ScrollWheel") * scrollSensetivity * transform.forward;
            transform.DOMove(newPosition, 3f).SetSpeedBased().SetEase(Ease.OutSine);
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            transform.eulerAngles += Vector3.up * rotationSensetivity * Input.GetAxis("Mouse X");
        }

    }
}
