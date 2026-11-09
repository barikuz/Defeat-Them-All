using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 1.5f; // Time between attacks

    private float lastAttackTime;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (attackPoint == null) return;

        if (Time.time >= lastAttackTime + attackCooldown && Attack())
        {
            lastAttackTime = Time.time;
        }
    }

    bool Attack()
    {
        Collider2D[] hitPlayers = GetPlayersInAttackRange();

        bool hitPlayer = false;

        foreach (Collider2D player in hitPlayers)
        {
            if (player.GetComponentInParent<PlayerHealth>() != null)
            {
                hitPlayer = true;
                break;
            }
        }

        if (hitPlayer)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Enemy attacks!");
        }

        return hitPlayer;
    }

    public void DealAttackDamage()
    {
        // Detect player in range using a circle overlap
        Collider2D[] hitPlayers = GetPlayersInAttackRange();

        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth health = player.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    private Collider2D[] GetPlayersInAttackRange()
    {
        return Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            playerLayer
        );
    }

    // Visualize the enemy attack range in the editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}