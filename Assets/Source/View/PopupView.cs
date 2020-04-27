using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupView : UIViewBase
{
    public event Action ConfirmButtonClicked;

    [SerializeField]
    private Text m_title;
    [SerializeField]
    private Text m_desc;
    [SerializeField]
    private Text m_confirmButtonName;
    [SerializeField]
    private GameObject m_backgroundPanel;
    [SerializeField]
    private Button m_confirmButton;

    void Start()
    {
        m_confirmButton.onClick.AddListener(() => { ConfirmButtonClicked(); });
    }

    public override void Hide()
    {
        base.Hide();
        m_title.text = "";
        m_desc.text = "";
        m_confirmButtonName.text = "";
        m_backgroundPanel.SetActive(false);
        ConfirmButtonClicked = null;
    }

    public override void Show()
    {
        ConfirmButtonClicked += Hide;
        base.Show();
    }

    public void Init(PopupInfoVO _vo)
    {
        m_title.text = _vo.title;
        m_desc.text = _vo.description;
        m_confirmButtonName.text = _vo.buttonName;
        m_backgroundPanel.SetActive(_vo.preventOtherInteractions);
        ConfirmButtonClicked += _vo.buttonAction;
    }
}
