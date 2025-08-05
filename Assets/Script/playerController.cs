using UnityEngine;

public class playerController : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement")]
    public float runSpeed = 10f;
    public FixedJoystick joystick;

    [Header("Jump")]
    public float jumpHeight = 2.5f;
    public float gravity = -9.81f;
    public float fallGravityMultiplier = 2f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float coyoteCounter = 0f;
    private float jumpBufferCounter = 0f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool jumpRequested = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check using sphere
        Vector3 groundCheckPos = controller.bounds.center + Vector3.down * (controller.bounds.extents.y + 0.05f);
        isGrounded = Physics.CheckSphere(groundCheckPos, groundCheckDistance, groundLayer);

        // Horizontal movement from joystick
        Vector3 moveInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
        Vector3 move = moveInput * runSpeed;

        // Handle coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Handle jump buffer
        if (jumpRequested)
        {
            jumpBufferCounter = jumpBufferTime;
            jumpRequested = false;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Apply jump
        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpBufferCounter = 0f;
        }

        // Apply gravity
        if (!isGrounded)
        {
            float gravityMultiplier = velocity.y < 0 ? fallGravityMultiplier : 1f;
            velocity.y += gravity * gravityMultiplier * Time.deltaTime;
        }
        else if (velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        // Final move
        Vector3 finalVelocity = move + velocity;
        controller.Move(finalVelocity * Time.deltaTime);

        // Rotate character toward movement
        if (moveInput != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveInput);

        // Keep player above Y=0
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            velocity.y = 0f;
        }
    }

    // Call this from UI button
    public void OnJumpButtonPressed()
    {
        jumpRequested = true;
    }

    // Visualize ground check
    void OnDrawGizmosSelected()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.down * (controller.height / 2f - 0.05f), groundCheckDistance);
    }
}
