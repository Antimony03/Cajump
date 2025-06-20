using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Jump")]
    public int maxJumps = 2;
    private int jumpCount = 0;
    private bool isGrounded;
    private bool wasGroundedLastFrame = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Collider Settings")]
    public Vector2 idleColliderSize = new Vector2(0.3547031f, 0.5102472f);
    public Vector2 idleColliderOffset = new Vector2(-0.0472f, -0.0836f);
    public Vector2 runColliderSize = new Vector2(0.4178f, 0.2161f);
    public Vector2 runColliderOffset = new Vector2(-0.0472f, -0.0836f); // Same offset assumed for run

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (moveX != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);

        // Ground check for animation (still needed visually)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }

        // Animations
        anim.SetBool("IsRunning", moveX != 0 && isGrounded);
        anim.SetBool("IsJumping", !isGrounded && rb.linearVelocity.y > 0);
        anim.SetBool("IsFalling", !isGrounded && rb.linearVelocity.y < 0);

        // Collider switching
        if (moveX != 0 && isGrounded)
        {
            boxCollider.size = runColliderSize;
            boxCollider.offset = runColliderOffset;
        }
        else
        {
            boxCollider.size = idleColliderSize;
            boxCollider.offset = idleColliderOffset;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player lands on ground, reset jump
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            GameOver();
        }
    }

    // Debug GroundCheck Circle
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    
    void GameOver()
    {
        FindObjectOfType<GameOverManager>()?.TriggerGameOver();
    }

}
