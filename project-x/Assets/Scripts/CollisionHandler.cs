using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool isCameraChanged = false;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    public Transform respawnLocation;  // 리스폰 위치

    void Start()
    {
        // "RespawnPoint"라는 오브젝트를 찾아 리스폰 위치로 설정
        respawnLocation = GameObject.Find("RespawnPoint").transform;

        // 초기 카메라 위치 저장
        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 몬스터와 충돌했을 때 (리스폰 처리)
        if (other.CompareTag("Monster"))
        {
            Debug.Log("몬스터와 충돌! 리스폰 처리");

            ChangeCameraView();  // 카메라 전환
            isCameraChanged = true;

            // 3초 후 카메라 복구
            Invoke("ResetCameraView", 3f);
        }

        // **회전하는 날개(장애물)와 충돌했을 때**
        if (other.CompareTag("Obstacle"))  // "Obstacle" 태그는 날개에 설정
        {
            Debug.Log("장애물에 부딪힘! 통과 불가능");

            // 장애물과 부딪힌 순간, Rigidbody의 속도를 0으로 설정하여 밀리지 않도록 함
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void ChangeCameraView()
    {
        // 카메라를 리스폰 지역으로 이동
        Camera.main.transform.position = respawnLocation.position + new Vector3(0, 80, 0);
        Camera.main.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void ResetCameraView()
    {
        // 카메라를 원래 위치로 복구
        Camera.main.transform.position = initialCameraPosition;
        Camera.main.transform.rotation = initialCameraRotation;
        isCameraChanged = false;
    }
}
