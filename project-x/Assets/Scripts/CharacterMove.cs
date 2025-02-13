using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float moveSpeed = 50f;
    public float jumpSpeed = 10f;
    public float gravity = -9.81f;
    private float yVelocity = 0;

    private Vector3 currentVelocity;
    public float smoothTime = 0.1f;

    private bool jumpInput = false;
    private bool isJumping = false;
    public float groundCheckDistance = 0.1f;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 스페이스바 입력 감지
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpInput = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v).normalized;
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        bool isGrounded = CheckGrounded();

        if (isGrounded)
        {
            yVelocity = -0.5f;
            isJumping = false;

            if (jumpInput)
            {
                yVelocity = jumpSpeed;
                isJumping = true;
                jumpInput = false;
            }
        }
        else
        {
            yVelocity += gravity * Time.fixedDeltaTime;
        }

        moveDirection.y = yVelocity;

        Vector3 smoothedMovement = Vector3.SmoothDamp(characterController.velocity, moveDirection, ref currentVelocity, smoothTime);
        
        characterController.Move(smoothedMovement * Time.fixedDeltaTime);
    }

    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f);
    }
}
