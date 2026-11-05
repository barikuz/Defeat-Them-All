using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    // Called by the Player Input component when the Attack action is pressed.
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("PlayerCombat requires an Attack Point reference.", this);
            return;
        }

        // Detect enemies in range of the attack point using a circle overlap
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage or destroy each detected enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);

            // Notify the GameManager that an enemy has been defeated
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemyDefeated();
            }
            
            // For now, destroy the enemy instantly on hit
            Destroy(enemy.gameObject);
        }
    }

    // Draw the attack range in the editor for visualization
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}