﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class McuItemView : UIViewBase
{
    [SerializeField]
    private Text m_mcuNameText;
    [SerializeField]
    private Text m_roomIdText;
    [SerializeField]
    private Image m_statusIndicator;
    [SerializeField]
    private Toggle m_expandToggle;
    [SerializeField]
    private GameObject m_expandPanel;

    private McuVO m_mcuVO;

    private void Start()
    {
        m_expandToggle.onValueChanged.AddListener((bool _isOn) => { OnExpandToggled(_isOn); });
    }

    private void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(McuItemViewMediator.NAME);
    }

    public McuItemView Init(McuVO _vo)
    {
        AppFacade.instance.RegisterMediator(new McuItemViewMediator(this, McuItemViewMediator.NAME + _vo.mcuName));

        UpdateMcuVO(_vo);

        return this;
    }

    public void UpdateMcuVO(McuVO _vo)
    {
        m_mcuVO = _vo;

        UpdateMcuName(_vo.mcuName);
        UpdateRoomID(_vo.roomID);
        UpdateStatus(_vo.mcuStatus);
    }

    public void UpdateMcuName(string _name)
    {
        m_mcuNameText.text = _name;
    }

    public void UpdateRoomID(string _id)
    {
        m_roomIdText.text = _id;
    }

    public void UpdateStatus(McuStatus _status)
    { 
        switch(_status)
        {
            case McuStatus.Unknown:
                m_statusIndicator.color = Color.grey;
                break;
            case McuStatus.Connected:
                m_statusIndicator.color = Color.cyan;
                break;
            case McuStatus.Disconnnected:
                m_statusIndicator.color = Color.yellow;
                break;
        }
    }

    private void OnExpandToggled(bool _isOn)
    {
        if (_isOn)
        {
            OnOpenExpandPanel();
        }
        else
        {
            OnCloseExpandPanel();
        }
    }

    private void OnOpenExpandPanel()
    {
        m_expandPanel.SetActive(true);
        //TODO: request for control infos(get all module attached to this mcu)
    }

    private void OnCloseExpandPanel()
    {
        m_expandPanel.SetActive(false);
    }
}
