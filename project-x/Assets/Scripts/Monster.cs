using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject dropItemPrefab;  // 드롭할 아이템의 프리팹
    public float dropChance = 0.5f;    // 아이템 드롭 확률 (0.5 = 50%)

    public void Die()
    {
        // 몬스터 사라지기 전에 아이템 드롭
        DropItem();

        // 몬스터 오브젝트 삭제
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if (Random.value < dropChance) // 랜덤 확률로 아이템 드롭
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity); // 몬스터 위치에 아이템 생성
        }
    }
}
