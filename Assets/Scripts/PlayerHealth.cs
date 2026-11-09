using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] private float deathDelay = 1.5f;
    private bool isDead;

    private Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;

        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathSequence());
        }
        else
        {
            animator.SetTrigger("Hurt");
        }

    }

    void Die()
    {
        Debug.Log("Player died! Restarting level...");
        // Reload the current scene on death
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private IEnumerator DeathSequence()
    {
        isDead = true;
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(deathDelay);

        Die();
    }
}