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


    private void Awake()
    {
        RememberMeId = PlayerPrefs.GetString("RememberMeId");

        if (PlayFabManager.Instance.isLogged == false && RememberMeId != null) { 
            AutoLogin(); //Frictionless Login
        } else if (PlayFabManager.Instance.isLogged == true)
        {
            //da aggiungere un cambio di menù nelle opzioni in caso il giocatore sia loggato
        }
        else
        {
            StartCoroutine(MostraScrittaPerDueSecondiCoroutine("Accedere per salvare il punteggio"));
        }
    }
    IEnumerator MostraScrittaPerDueSecondiCoroutine(string stringa)
    {
        // Attiva il GameObject della scritta
        WelcomeObject.SetActive(true);

        //Testo di benvenuto
        WelcomeText.text = stringa;

        // Attendi per 2 secondi
        yield return new WaitForSeconds(2.0f);

        // Disattiva il GameObject della scritta dopo l'attesa
        WelcomeObject.SetActive(false);
    }

    private void AutoLogin()
    {

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

            StartCoroutine(MostraScrittaPerDueSecondiCoroutine("Bentornato" + name));
 
            // Puoi accedere alle informazioni sull'account attraverso result.PlayFabId, result.SessionTicket, ecc.
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

    public void LogOut()
    {
        if (PlayFabManager.Instance.isLogged == true)
        {
            PlayFabManager.Instance.effettuaLogout();
        }
        else
        {
            Debug.Log("Not Logged In");
        }
    }
}
