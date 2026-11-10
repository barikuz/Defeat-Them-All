using UnityEngine;
using UnityEngine.SceneManagement; // Required to load scenes
using System.Collections;

public class ExitDoor : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Level2"; // Name of the next level scene

    private bool isLoading;

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
        isLoading = true;

        yield return new WaitForSeconds(delay);

        Debug.Log("Level completed! Loading next level...");
        SceneManager.LoadScene(nextSceneName);
    }
}