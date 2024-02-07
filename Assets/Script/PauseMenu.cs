using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject leaderboardMessage;

    public void Pause( )
    {
        pauseMenu.SetActive(true);
        scoreUI.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }


      public void LeaderBoardScreen()
    {
        if (PlayFabManager.Instance.isLogged == true)
        {
            SceneManager.LoadSceneAsync(3);
            Time.timeScale = 1;
        } else
        {
            leaderboardMessage.SetActive(true);
            //StartCoroutine(DeactivateAfterDelay(2f)); non funziona perché PauseMenu viene disattivato al gameover
        }
    }
    IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendere per il tempo specificato
        leaderboardMessage.SetActive(false); // Disattiva il GameObject
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

