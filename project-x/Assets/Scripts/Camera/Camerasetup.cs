using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(0, 3, -14);
    public Vector3 rotationOffset = new Vector3(10, 0, 0);

    void Awake()
    {
        SetCameraPosition();
    }

    void OnValidate()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        transform.localPosition = positionOffset;
        transform.localEulerAngles = rotationOffset;
    }
}

