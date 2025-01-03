using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool isCameraChanged = false;
    private Vector3 initialCameraPosition; // 초기 카메라 위치
    private Quaternion initialCameraRotation; // 초기 카메라 회전
    public Transform respawnLocation;  // 리스폰 지역의 Transform (위에서 내려다보는 위치 설정)

    void Start()
    {
        // 씬에서 "RespawnPoint"라는 이름을 가진 게임 오브젝트를 찾아 respawnLocation에 할당
        respawnLocation = GameObject.Find("RespawnPoint").transform;

        // 카메라의 초기 위치와 회전을 저장
        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!isCameraChanged)
            {
                ChangeCameraView(); // 카메라를 리스폰 지역을 고정적으로 바라보게 변경
                isCameraChanged = true;

                // 3초 뒤 카메라 복구
                // Invoke("ResetCameraView", 3f);
            }
        }
    }

    private void ChangeCameraView()
    {
        // 카메라를 리스폰 지역 위에서 내려다보는 시점으로 설정
        Camera.main.transform.position = respawnLocation.position + new Vector3(0, 15, -10);  // 리스폰 지역에서 15만큼 위, 10만큼 뒤로
        Camera.main.transform.rotation = Quaternion.Euler(60, 0, 0);  // 카메라를 아래로 내려다보는 각도로 설정
    }

    public void ResetCameraView()
    {
        // 카메라를 초기 위치로 되돌림
        Camera.main.transform.position = initialCameraPosition;
        Camera.main.transform.rotation = initialCameraRotation;
        isCameraChanged = false;
    }
}
