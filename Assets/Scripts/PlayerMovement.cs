using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    private readonly HashSet<Collider2D> groundColliders = new();

    private PlayerAudio playerAudio;

    private Vector2 mobileMoveInput;

    private Vector2 activeMoveInput =>
    Mathf.Abs(mobileMoveInput.x) > 0.01f
        ? mobileMoveInput
        : moveInput;

    void Start()
    {
        // Get the Rigidbody2D attached to the player.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerAudio = GetComponentInChildren<PlayerAudio>();
    }

    void Update()
    {
        if (Mathf.Abs(activeMoveInput.x) > Mathf.Epsilon)
        {
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (activeMoveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(activeMoveInput.x);
            transform.localScale = scale;
        }
    }

    public void SetMobileMoveInput(Vector2 input)
    {
        mobileMoveInput = input;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Mathf.Abs(mobileMoveInput.x) > 0.01f
        ? mobileMoveInput.x
        : moveInput.x;

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
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

            playerAudio.PlayJump();

            animator.SetBool("Grounded", false);
            animator.SetTrigger("Jump");

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateGroundedState(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        UpdateGroundedState(collision);
    }

    private void UpdateGroundedState(Collision2D collision)
    {
        if (!IsGroundLayer(collision.gameObject))
        {
            return;
        }

        // Ignore lingering contacts while the player is moving upward after a jump.
        if (rb.linearVelocity.y > 0.01f)
        {
            return;
        }
         
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check if the contact normal indicates that the player is standing on top of the ground.
            if (contact.normal.y > 0.5f)
            {
                groundColliders.Add(collision.collider);
                isGrounded = true;
                animator.SetBool("Grounded", true);
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!IsGroundLayer(collision.gameObject))
        {
            return;
        }

        groundColliders.Remove(collision.collider);

        if (groundColliders.Count == 0)
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);
        }
    }

    private bool IsGroundLayer(GameObject other)
    {
        // Check whether the object uses the Ground layer.
        return other.layer == LayerMask.NameToLayer("Ground");
    }
}