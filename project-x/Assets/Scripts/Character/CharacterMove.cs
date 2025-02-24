using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float maxSpeed = 50f;
    public float acceleration = 20f;
    public float deceleration = 15f;
    public float jumpForce = 20f;
    public float jumpCooldown = 0.5f;
    public Transform cameraTransform;
    
    public float slideThreshold = 10f;
    public float gravityMultiplier = 1f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 currentVelocity;
    private bool jumpRequested = false;
    private float lastJumpTime;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastJumpTime = -jumpCooldown;
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time >= lastJumpTime + jumpCooldown)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        Vector3 targetVelocity = moveDirection * maxSpeed;

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            (moveDirection.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        ApplySlope();

        // 점프 처리
        if (jumpRequested && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
            lastJumpTime = Time.time;
        }

        // 수평 속도만 설정
        rb.linearVelocity = new Vector3(currentVelocity.x, rb.linearVelocity.y, currentVelocity.z);

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + extraHeight);
    }

    private void ApplySlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            if (slopeAngle > slideThreshold)
            {
                Vector3 slopeDirection = Vector3.ProjectOnPlane(Physics.gravity, hit.normal).normalized;
                float slideForce = Physics.gravity.magnitude * gravityMultiplier * Mathf.Sin(slopeAngle * Mathf.Deg2Rad);
                
                // 미끄러짐 힘 증가
                slideForce *= 1.5f;
                
                Vector3 slideVelocity = slopeDirection * slideForce;

                // 현재 속도에 미끄러짐 속도를 더 강하게 적용
                currentVelocity += slideVelocity * Time.fixedDeltaTime * 2f;

                // 마찰력 감소
                float frictionCoefficient = Mathf.Lerp(0.1f, 0.01f, slopeAngle / 90f);
                currentVelocity *= frictionCoefficient;

                // 경사면 방향으로 캐릭터 회전
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }

        // 수직 속도 제한 (옵션)
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -20f, 20f);
    }
}
