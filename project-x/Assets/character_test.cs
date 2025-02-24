using UnityEngine;

public class character_test : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;
    public Transform cameraTransform;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Vector3 moveDirection;
    private bool isGrounded;
    private float friction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // 입력 처리
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // 점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
        CheckGrounded();
        ApplyFriction();
    }

    void Move()
    {
        Vector3 movement = moveDirection * moveSpeed;
        rb.AddForce(movement, ForceMode.Acceleration);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + capsuleCollider.height / 2, groundMask);
    }

    void ApplyFriction()
    {
        if (isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance + capsuleCollider.height / 2, groundMask))
            {
                PhysicsMaterial material = hit.collider.material;
                if (material != null)
                {
                    friction = rb.linearVelocity.magnitude > 0.01f ? material.dynamicFriction : material.staticFriction;
                }
                else
                {
                    friction = 0.6f; // 기본 마찰 계수
                }

                // 마찰력 적용
                Vector3 frictionForce = -rb.linearVelocity * friction;
                rb.AddForce(frictionForce, ForceMode.Acceleration);
            }
        }
    }
}
