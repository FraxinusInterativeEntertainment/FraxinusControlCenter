using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginView : UIViewBase
{
    public event Action TryLogin = delegate { };
    public event Action TryLogout = delegate { };

    public LoginVO loginVO { get; private set; }

    [SerializeField]
    private GameObject m_userInfoPanel;
    [SerializeField]
    private Button m_logoutButton;

    [SerializeField]
    private GameObject m_loginPanel;
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

        m_logoutButton.onClick.AddListener(() => { TryLogout(); });

        loginVO = new LoginVO();
    }
    
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(LoginViewMediator.NAME);
    }

    public void ActivateLoginPanel()
    {
        m_userInfoPanel.SetActive(false);
        m_loginPanel.SetActive(true);
        ClearUI();
    }

    public void ActivateUserInfoPanel()
    {
        m_userInfoPanel.SetActive(true);
        m_loginPanel.SetActive(false);
        ClearUI();
    }

    private void ClearUI()
    {
        m_userNameField.text = "";
        m_passwordField.text = "";
    }
}
