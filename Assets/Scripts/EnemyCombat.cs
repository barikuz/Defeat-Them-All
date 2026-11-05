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
        // Detect player in range using a circle overlap
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        bool hitPlayer = false;

        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                hitPlayer = true;
                health.TakeDamage(attackDamage);
            }
        }

        if (hitPlayer)
        {
            Debug.Log("Enemy attacks!");
        }

        return hitPlayer;
    }

    // Visualize the enemy attack range in the editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}