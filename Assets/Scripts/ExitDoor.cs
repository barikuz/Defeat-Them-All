using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes
using System.Collections;

public class ExitDoor : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Level2"; // Name of the next level scene
    [SerializeField] private bool isFinalLevel;
    [SerializeField] private GameObject youWinCanvas;
    
    private bool isLoading;

    [SerializeField] private int nextLevelToUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryStartLevelTransition(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryStartLevelTransition(collision);
    }

    private bool IsDoorOpen()
    {
        Animator animator = GetComponent<Animator>();

        if (animator == null) return false;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        
        return state.IsName("DoorOpened") && state.normalizedTime >= 1f;

    }

    private void TryStartLevelTransition(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (IsDoorOpen() && !isLoading)
        {
            StartCoroutine(LoadNextLevelAfterDelay(1f));
        }
    }
    
    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        if (nextLevelToUnlock > 0)
        {
             int currentHighestLevel =
            PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

            if (nextLevelToUnlock > currentHighestLevel)
            {
                PlayerPrefs.SetInt("HighestUnlockedLevel", nextLevelToUnlock);
                PlayerPrefs.Save();
            }
        
        }
       
        isLoading = true;

        yield return new WaitForSeconds(delay);

        if (isFinalLevel)
        {
            if (youWinCanvas != null)
            {
                youWinCanvas.SetActive(true);
            }

            yield break;
        }

        Debug.Log("Level completed! Loading next level...");
        SceneManager.LoadScene(nextSceneName);
    }

    public void PlayAgain()
    {
        PlayerHealth.ResetHealth();
        SceneManager.LoadScene(1);
    }
}