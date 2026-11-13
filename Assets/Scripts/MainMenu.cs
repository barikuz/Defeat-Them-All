using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject levelCanvas;

    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        levelCanvas.SetActive(false);
    }

    public void PlayGame()
    {
        mainMenuCanvas.SetActive(false);
        levelCanvas.SetActive(true);

        int highestUnlockedLevel =
            PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

        level1Button.interactable = highestUnlockedLevel >= 1;
        level2Button.interactable = highestUnlockedLevel >= 2;
        level3Button.interactable = highestUnlockedLevel >= 3;
    }

    public void BackToMainMenu()
    {
        levelCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}