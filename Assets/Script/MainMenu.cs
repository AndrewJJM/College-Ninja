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
    [SerializeField]
    private GameObject leaderboardButton;
    [SerializeField]
    private GameObject logoutButton;
    [SerializeField]
    private GameObject mainMenuGUI;
    [SerializeField]
    private GameObject muroSopra;
    [SerializeField]
    private GameObject muroSotto;
    [SerializeField]
    private GameObject muroVuoto;



    private void Awake()
    {
        RememberMeId = PlayerPrefs.GetString("RememberMeId");

        if (PlayFabManager.Instance.isLogged == false && !string.IsNullOrEmpty(RememberMeId)) { 
            AutoLogin(); //Frictionless Login
        }
        else if (PlayFabManager.Instance.isLogged == true)
        {
            //da aggiungere un cambio di menù nelle opzioni in caso il giocatore sia loggato
        }
        else
        {
            leaderboardButton.SetActive(false);
            logoutButton.SetActive(false);
            StartCoroutine(MostraScrittaPerDueSecondiCoroutine("Accedere per salvare il punteggio"));
        }

    }

    private void Start()
    {
        
    }

    IEnumerator MostraScrittaPerDueSecondiCoroutine(string stringa)
    {
        // Attiva il GameObject della scritta
        WelcomeObject.SetActive(true);

        LeanTween.moveY(WelcomeObject, WelcomeObject.transform.position.y -100, 0.4f);

        //Testo di benvenuto
        WelcomeText.text = stringa;

        // Attendi per 2 secondi
        yield return new WaitForSeconds(2.0f);

        // Disattiva il GameObject della scritta dopo l'attesa
        LeanTween.moveY(WelcomeObject, WelcomeObject.transform.position.y + 100, 0.4f);
        yield return new WaitForSeconds(0.4f);
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
            string name = null;

            if (result.InfoResultPayload != null)
            {
                name = result.InfoResultPayload.PlayerProfile.DisplayName;
            }
            PlayFabManager.Instance.isLogged = true;
            PlayFabManager.Instance.currentLoggedId = result.PlayFabId;



            StartCoroutine(MostraScrittaPerDueSecondiCoroutine("Bentornato " + name));
 
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
            leaderboardButton.SetActive(false);
            logoutButton.SetActive(false);
        }
        else
        {
            Debug.Log("Not Logged In");
        }
    }
    public void OpenLeaderboard()
    {
        SceneManager.LoadSceneAsync(3); //numero da cambiare in base al numero scena nella build
    }
}
