using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    private void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}