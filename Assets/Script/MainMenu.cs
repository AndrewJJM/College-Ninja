using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
     }


public void QuitGame()
    {
        Application.Quit();
    }

    public void LoginScene()
    {
        SceneManager.LoadSceneAsync(2); //numero da cambiare in base al numero scena nella build
    }
}
