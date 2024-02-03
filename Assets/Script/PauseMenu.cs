using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject pauseButton;

    public void Pause( )
    {
        pauseMenu.SetActive(true);
        scoreUI.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }


    public void LeaderBoardScreen()
    {
        SceneManager.LoadSceneAsync(3);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        scoreUI.SetActive(true);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}

