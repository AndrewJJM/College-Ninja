using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

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



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Buttom Functions
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
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        MessageText.text = "Login in";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
