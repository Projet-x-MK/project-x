using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 3.0f; // 몬스터 이동 속도
    
    [SerializeField]
    private Vector3[] waypoints = new Vector3[] {
        new Vector3(200, 5, 0),  // 첫 번째 지점
        new Vector3(230, 5, 0),  // 두 번째 지점
        new Vector3(170, 5, 0),  // 세 번째 지점
        new Vector3(200, 5, -30),  // 네 번째 지점
        new Vector3(230, 5, 30),  // 다섯 번째 지점
    };
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        // 현재 목표 지점으로 이동
        Vector3 target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 목표 지점에 도달하면 다음 지점으로 변경
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
