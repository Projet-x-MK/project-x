using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 회전 속도

    void Update()
    {
        // Y축을 중심으로 회전하도록 수정 (선풍기처럼 회전)
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
