using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;

    // References the player's physics component and current movement input.
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private Animator animator;

    void Start()
    {
        // Get the Rigidbody2D attached to the player.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Apply horizontal movement while preserving the current vertical velocity.
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x) * Mathf.Sign(moveInput.x);
            transform.localScale = scale;
        }
    }

    public void OnMove(InputValue value)
    {
        // Read the movement direction provided by the Player Input component.
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // Allow jumping only when the jump button is pressed and the player is grounded.
        if (value.isPressed && isGrounded)
        {
            // Prevent another jump until the player lands again.
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mark the player as grounded when it touches the Ground layer.
        if (IsGroundLayer(collision.gameObject))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // The player is airborne after leaving the Ground layer.
        if (IsGroundLayer(collision.gameObject))
        {
            isGrounded = false;
        }
    }

    private bool IsGroundLayer(GameObject other)
    {
        // Check whether the object uses the Ground layer.
        return other.layer == LayerMask.NameToLayer("Ground");
    }
}