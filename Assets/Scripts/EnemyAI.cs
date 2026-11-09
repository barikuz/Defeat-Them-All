using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 5f; // The distance required to start chasing the player

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        // Find the player in the scene using the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // If there is no player reference, stop running the code
        if (playerTransform == null) return;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            // Stop moving when the player is out of range
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetInteger("AnimState", 0);
        }
    }

    void ChasePlayer()
    {
        // Determine the direction towards the player (1 for right, -1 for left)
        float direction = playerTransform.position.x > transform.position.x ? 1f : -1f;

        // Move horizontally towards the player
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        animator.SetInteger("AnimState", 2);

        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}