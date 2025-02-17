using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float moveSpeed = 50f;
    public float jumpSpeed = 30f;
    public float gravity = -50f;
    private float yVelocity = 0;

    private Vector3 currentVelocity;
    public float smoothTime = 0.1f;

    private bool isJumping = false;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 점프 입력 처리
        if (Input.GetButtonDown("Jump") && !isJumping && characterController.isGrounded)
        {
            yVelocity = jumpSpeed;
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(h, 0, v).normalized;
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // 지면 체크 및 중력 적용
        if (characterController.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
            isJumping = false;
        }
        else
        {
            yVelocity += gravity * Time.fixedDeltaTime;
        }

        // 최종 이동 벡터 계산
        Vector3 movement = new Vector3(moveDirection.x, yVelocity, moveDirection.z);

        // 수평 이동에만 스무딩 적용
        Vector3 smoothedMovement = Vector3.SmoothDamp(
            new Vector3(characterController.velocity.x, 0, characterController.velocity.z),
            new Vector3(movement.x, 0, movement.z),
            ref currentVelocity,
            smoothTime
        );

        // 최종 이동 벡터 조합 (스무딩된 수평 이동 + 수직 속도)
        movement = new Vector3(smoothedMovement.x, movement.y, smoothedMovement.z);

        // 캐릭터 이동
        characterController.Move(movement * Time.fixedDeltaTime);
        //
    }
}
