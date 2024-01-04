using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.Internal;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TopText;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLoginInput;
    [SerializeField] TMP_InputField PasswordLoginInput;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField UsernameRegisterInput;
    [SerializeField] TMP_InputField EmailRegisterInput;
    [SerializeField] TMP_InputField PasswordRegisterInput;
    [SerializeField] GameObject RegisterPage;

    [Header("Recovery")]

    [SerializeField] TMP_InputField EmailRecoveryInput;
    [SerializeField] GameObject RecoveryPage;


    [SerializeField]
    private GameObject WelcomeObject;
    [SerializeField]
    private TextMeshProUGUI WelcomeText;


    private const string _PlayFabRememberMeIdKey = "RememberMeId";
    private string RememberMeId
    {
        get
        {
            return PlayerPrefs.GetString(_PlayFabRememberMeIdKey, "");
        }
        set
        {
            var guid = value ?? Guid.NewGuid().ToString();
            PlayerPrefs.SetString(_PlayFabRememberMeIdKey, guid);
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Button Functions
    public void RegisterUser()
    {
        // Se la password è minore di 6 caratteri genera un messaggio
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = UsernameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterInput.text,

            RequireBothUsernameAndEmail = false

        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSucces, OnError);
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailLoginInput.text,
            Password = PasswordLoginInput.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        string name = null;

        if (result.InfoResultPayload != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        WelcomeObject.SetActive(true);

        //Testo di benvenuto
        WelcomeText.text = "Welcome " + name;
        CreaRememberMeId();
        PlayFabManager.Instance.isLogged = true;
        StartCoroutine(LoadNextScene());
        
    }


    private void CreaRememberMeId()
    {
        PlayerPrefs.SetInt("RememberMe", 1);
        RememberMeId = Guid.NewGuid().ToString(); //questo codice lo salva anche nei playerPrefs grazie al costruttore

        // Fire and forget, but link a custom ID to this PlayFab Account.
        PlayFabClientAPI.LinkCustomID(
            new LinkCustomIDRequest
            {
                CustomId = RememberMeId,
                ForceLink = true
            },
            null,   // Success callback
            null    // Failure callback
            );


    }
    public void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = EmailRecoveryInput.text,
            TitleId = "6EE70",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFabError result)
    {
        MessageText.text = "No Email Found";
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult obj)
    {
        OpenLoginPage();
        MessageText.text = "Recovery Mail Sent";
    }

    private void OnError(PlayFabError Error)
    {
        MessageText.text = Error.ErrorMessage;
        Debug.Log(Error.GenerateErrorReport());
    }

    private void OnregisterSucces(RegisterPlayFabUserResult Result)
    {
        MessageText.text = "New Account Is Created";
        CreaRememberMeId();
    }

    public void OpenLoginPage()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(false);
        TopText.text = "Login";
    }
    public void OpenRegisterPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        RecoveryPage.SetActive(false);
        TopText.text = "Register";
    }
    public void OpenRecoveryPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(true);
        TopText.text = "Recovery";
    }
    #endregion


    public void ClearRememberMe()
    {
        PlayerPrefs.DeleteKey(_PlayFabRememberMeIdKey);
    }


    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2);
        MessageText.text = "Loggin in";
        SceneManager.LoadSceneAsync(1); //da modificare in caso di ordine cambiato
    }
}
