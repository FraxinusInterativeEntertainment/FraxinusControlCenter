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

    // Start is called before the first frame update
    void Start()
    {
        AppFacade.instance.RegisterMediator(new LoginViewMediator(this));

        m_loginButton.onClick.AddListener(() => { TryLogin(); });
        m_userNameField.onValueChanged.AddListener((string _userName) => { loginVO.userName = _userName; });
        m_passwordField.onValueChanged.AddListener((string _password) => { loginVO.password = _password; });

        loginVO = new LoginVO();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(LoginViewMediator.NAME);
    }

    public void ReceiveMessage(object vo)
    {
    }
    
}
