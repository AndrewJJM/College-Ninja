using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int RememberMe;
    private string RememberMeId;
    [SerializeField]
    private GameObject WelcomeObject;
    [SerializeField]
    private TextMeshProUGUI WelcomeText;

    private void Start()
    {
        if (PlayFabManager.Instance.isLogged == false) { 
            AutoLogin(); //Frictionless Login
        }
    }

    private void AutoLogin()
    {
        RememberMeId = PlayerPrefs.GetString("RememberMeId");

        var request = new LoginWithCustomIDRequest
        {
            TitleId = PlayFabSettings.TitleId,
            CustomId = RememberMeId,
            CreateAccount = true, // Crea un account se non esiste ancora
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
        {
            Debug.Log("Login con successo!");
            string name = null;

            if (result.InfoResultPayload != null)
            {
                name = result.InfoResultPayload.PlayerProfile.DisplayName;
            }
            PlayFabManager.Instance.isLogged = true;

            StartCoroutine(MostraScrittaPerDueSecondiCoroutine(name));
 
            // Puoi accedere alle informazioni sull'account attraverso result.PlayFabId, result.SessionTicket, ecc.
        }

        IEnumerator MostraScrittaPerDueSecondiCoroutine(string username)
        {
            // Attiva il GameObject della scritta
            WelcomeObject.SetActive(true);

            //Testo di benvenuto
            WelcomeText.text = "Bentornato " + username;

            // Attendi per 2 secondi
            yield return new WaitForSeconds(2.0f);

            // Disattiva il GameObject della scritta dopo l'attesa
            WelcomeObject.SetActive(false);
        }

        void OnLoginFailure(PlayFab.PlayFabError error)
        {
            Debug.LogError("Errore durante il login: " + error.GenerateErrorReport());
        }
    }

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
