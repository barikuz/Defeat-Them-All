using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayers;

    private Animator animator;

    [SerializeField] private float deathDelay = 1.5f;

    private readonly HashSet<GameObject> dyingEnemies = new();
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Called by the Player Input component when the Attack action is pressed.
    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
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

            if (!dyingEnemies.Add(enemy.gameObject))
            {
                continue;
            }

            StartCoroutine(DefeatEnemy(enemy.gameObject));
        }
    }

    private IEnumerator DefeatEnemy(GameObject enemy)
    {
        Animator enemyAnimator = enemy.GetComponentInChildren<Animator>();

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Death");
        }


        yield return new WaitForSeconds(deathDelay);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        Destroy(enemy);
    }

    // Draw the attack range in the editor for visualization
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}