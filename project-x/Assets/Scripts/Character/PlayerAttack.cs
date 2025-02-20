using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 8f;  // 공격 범위 설정
    public Camera playerCamera;     // 플레이어 카메라

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭을 감지
        if (Input.GetMouseButtonDown(0))  // 0은 마우스 왼쪽 버튼
        {
            Attack();
        }
    }

    void Attack()
    {
        // 카메라의 위치에서 forward 방향으로 Ray를 쏴서 충돌한 객체를 감지
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);  // 마우스 위치에서 Ray 발사
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))  // Raycast를 발사해서 충돌한 객체가 있는지 확인
        {
            if (hit.collider.CompareTag("Monster"))  // 몬스터 태그가 있는 객체인지 확인
            {
                // 몬스터의 Die() 함수 호출
                Monster monster = hit.collider.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.Die();  // 몬스터가 클릭되면 사라짐
                }
            }
        }
    }
}
