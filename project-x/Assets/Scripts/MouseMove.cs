using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float horizontalSensitivity = 1000f; // 수평 감도 증가
    public float verticalSensitivity = 1000f;   // 수직 감도 증가
    public float rotationX;
    public float rotationY;

    public float minVerticalAngle = -90f; // 수직 회전 최소 각도
    public float maxVerticalAngle = 90f;  // 수직 회전 최대 각도

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
    }

    void Update()
    {
        float mouseMoveX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseMoveY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        rotationY += mouseMoveX;
        rotationX -= mouseMoveY; // 반전된 Y축 움직임

        // 수직 회전 각도 제한
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // 카메라 회전 적용
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
