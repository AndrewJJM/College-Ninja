using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGI TopText;
    [SerializeField] TextMeshProUGI MessageText;

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
}
