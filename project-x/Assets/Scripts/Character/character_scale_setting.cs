using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    // 고정할 위치, 회전, 크기 값
    private Vector3 fixedPosition = new Vector3(0, 1, 0); // 고정 위치
    private Quaternion fixedRotation = Quaternion.Euler(0, 0, 0); // 고정 회전
    private Vector3 fixedScale = new Vector3(1.8f, 1.8f, 1); // 고정 크기 (모든 축에 1.8)

    void Awake()
    {
        // Transform 값 고정
        transform.position = fixedPosition;
        transform.rotation = fixedRotation;
        transform.localScale = fixedScale;

        Debug.Log("Player Transform values applied.");
    }
}
