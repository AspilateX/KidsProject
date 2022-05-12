using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool CameraMovementLocked { get; set; } = false;
    public bool CameraRotationLocked { get; set; } = false;
    public bool CameraZoomingLocked { get; set; } = false;
    public CameraRotationMode CameraRotationMode { get; set; } = CameraRotationMode.Streight;

    [SerializeField]
    private float _movementSpeed = 3f;
    [SerializeField]
    private float minAltitude = 1f;
    [SerializeField]
    private float maxAltitude = 4f;
    [SerializeField]
    private float scrollSensetivity = 5f;
    [SerializeField]
    private float rotationSensetivity = 3f;

    private Vector3 _pivot = Vector3.zero;
    void Update()
    {
        //Movement
        if (!CameraMovementLocked)
        {
            Vector2 inputDir = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1f);
            Vector2 planarVelocity = inputDir * _movementSpeed;
            Vector3 forwardCross = Vector3.Cross(Vector3.up, transform.forward);
            Vector3 rightCross = Vector3.Cross(forwardCross, Vector3.up);

            Vector3 velocity = forwardCross * planarVelocity.x + rightCross * planarVelocity.y;

            transform.position += velocity * Time.deltaTime;
        }
        //Rotation
        if (!CameraRotationLocked)
        {
            if (Input.GetKey(KeyCode.Mouse2))
            {
                switch (CameraRotationMode)
                {
                    case CameraRotationMode.Streight:
                    {
                        transform.eulerAngles += Vector3.up * rotationSensetivity * Input.GetAxis("Mouse X");
                        break;
                    }
                    case CameraRotationMode.Orbital:
                    {
                        transform.RotateAround(_pivot, Vector3.up, rotationSensetivity * Input.GetAxis("Mouse X"));
                        break;
                    }
                }

            }
        }
        //Zooming
        if (!CameraZoomingLocked)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minAltitude ||
                Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxAltitude)
            {
                Vector3 newPosition = transform.position + Input.GetAxis("Mouse ScrollWheel") * scrollSensetivity * transform.forward;
                MoveTo(newPosition);
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.DOMove(position, 3f).SetSpeedBased().SetEase(Ease.OutSine);
    }
    public void SetPivotPoint(Vector3 pivot)
    {
        _pivot = pivot;
    }
}
public enum CameraRotationMode
{
    Orbital,
    Streight
}



