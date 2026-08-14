using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GatherInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool isGrounded;

    private Rigidbody2D rb;
    private GatherInput inputHandler;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<GatherInput>();
    }

    private void Update()
    {
        CheckGround();
        HandleJump();
        Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        rb.linearVelocity = new Vector2(inputHandler.valueX * speed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (inputHandler.isJump && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void CheckGround()
    {
        // ตรวจสอบพื้นด้วย GroundCheck (ถ้ามี) หรือตรวจจากความเร็วแกน Y
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else
        {
            isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.02f;
        }
    }

    private void Flip()
    {

        if (inputHandler.valueX > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputHandler.valueX < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}