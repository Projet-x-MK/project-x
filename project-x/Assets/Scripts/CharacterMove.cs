using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float maxSpeed = 50f;
    public float acceleration = 100f;
    public float deceleration = 100f;
    public float airDeceleration = 10f;
    public float gravity = -50f;
    public float jumpForce = 15f;

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = cameraTransform.right * x + cameraTransform.forward * z;
        move.y = 0f;
        move = Vector3.ClampMagnitude(move, 1f);

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        if (isGrounded)
        {
            if (move.magnitude > 0.1f)
            {
                // 방향 전환 시 속도 조정
                float alignmentFactor = Vector3.Dot(horizontalVelocity.normalized, move.normalized);
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, move * maxSpeed, (1 - alignmentFactor) * Time.deltaTime * 5f);
                
                // 가속도 적용
                horizontalVelocity += move * acceleration * Time.deltaTime;
            }
            else
            {
                // 감속 로직 개선
                horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, deceleration * Time.deltaTime);
            }
        }
        else
        {
            // 공중에서의 움직임
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, airDeceleration * Time.deltaTime);
        }

        // 속도 벡터 정규화 및 최대 속도 제한
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxSpeed);
        velocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
