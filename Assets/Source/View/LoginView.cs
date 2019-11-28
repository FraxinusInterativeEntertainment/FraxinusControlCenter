using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginView : MonoBehaviour
{
    public event Action TryLogin = delegate { };
    
    public LoginVO loginVO { get; private set; }

    [SerializeField]
    private Button m_loginButton;
    [SerializeField]
    private InputField m_userNameField;
    [SerializeField]
    private InputField m_passwordField;

    void Start()
    {
        AppFacade.instance.RegisterMediator(new LoginViewMediator(this));

        m_loginButton.onClick.AddListener(() => { TryLogin(); });
        m_userNameField.onValueChanged.AddListener((string _userName) => { loginVO.userName = _userName; });
        m_passwordField.onValueChanged.AddListener((string _password) => { loginVO.password = _password; });
        
        loginVO = new LoginVO();
    }
    
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(LoginViewMediator.NAME);
    }

    public void OnLoginSuccess(object vo)
    {
        Debug.Log(vo);
    }

    public void OnLoginFail(object vo)
    {
        Debug.Log(vo);
    }
}
