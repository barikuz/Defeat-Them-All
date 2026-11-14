using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] private float deathDelay = 1.5f;
    
    private bool isDead;
    [SerializeField] private GameObject loseCanvas;

    private Animator animator;
    [SerializeField] private Animator[] heartAnimators;

    private PlayerAudio playerAudio;
    
    private static int savedHealth = -1;

    void Start()
    {
        if (savedHealth == -1)
        {
            savedHealth = maxHealth;
        }

        currentHealth = savedHealth;

        for (int i = currentHealth; i < maxHealth; i++)
        {
            if (i < heartAnimators.Length)
            {
                heartAnimators[i].SetTrigger("HeartLost");
            }
        }

        Debug.Log("Level başladı. Can: " + currentHealth);

        animator = GetComponentInChildren<Animator>();
        playerAudio = GetComponentInChildren<PlayerAudio>();

        if (loseCanvas != null)
        {
            loseCanvas.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        playerAudio.PlayHit();

        currentHealth -= damage;

        savedHealth = currentHealth;

        int heartIndex = currentHealth;

        if (heartIndex >= 0 && heartIndex < heartAnimators.Length)
        {
            heartAnimators[heartIndex].SetTrigger("HeartLost");
        }

        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            DeathSequence();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }

    }

    public void PlayAgain()
    {
        ResetHealth();
        
        Debug.Log("Player died! Restarting level...");
        // Reload the current scene on death
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void DeathSequence()
    {
        isDead = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        PlayerCombat playerCombat = GetComponent<PlayerCombat>();
        PlayerInput playerInput = GetComponent<PlayerInput>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerCombat != null)
        {
            playerCombat.enabled = false;
        }

        if (playerInput != null)
        {
            playerInput.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        animator.SetTrigger("Death");

        if (loseCanvas != null)
        {
            loseCanvas.SetActive(true);
        }

    }

    public static void ResetHealth()
    {
        savedHealth = -1;
    }
}