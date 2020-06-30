using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoItemView : MonoBehaviour
{
    public event Action OnChangeCurrentGroupName = delegate { };
    [SerializeField]
    private Text m_UIDText;
    [SerializeField]
    private Text m_groupNameText;
    [SerializeField]
    private Image m_uwbTagIndicator;
    [SerializeField]
    private Image m_handDeviceIndicator;
    [SerializeField]
    private Toggle m_expandToggle;

    [SerializeField]
    private GameObject m_expandPanel;
    [SerializeField]
    private Text m_userNameText;
    [SerializeField]
    private Text m_nickNameText;
    [SerializeField]
    private Text m_bandIDText;
    [SerializeField]
    private Text m_tagIDText;

    [SerializeField]
    private Button m_cutGroupNameButton;
    [SerializeField]
    private Text m_targetGroupName;
    public PlayerInfoVO playerInfoVO;
    void Start()
    {
        m_expandToggle.onValueChanged.AddListener((bool _isOn) => { OnExpandToggled(_isOn); });
        m_cutGroupNameButton.onClick.AddListener(() => { OnSetCurrentGroupName(); });
    }
    private void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(UserInfoItemViewMediator.NAME);
    }
    public UserInfoItemView Init(PlayerInfo _vo)
    {
        AppFacade.instance.RegisterMediator(new UserInfoItemViewMediator(this, UserInfoItemViewMediator.NAME + _vo.uid));
        UpdateUserInfoVO(_vo);
        UpdateHandDeviceStatus(_vo.status);
        return this;
    }
    private void UpdateUserInfoVO(PlayerInfo _vo)
    {
        UpdateUIDText(_vo.uid);
        m_userNameText.text = _vo.uid;
        m_nickNameText.text = _vo.nickName;
        m_bandIDText.text = _vo.bid;
        m_tagIDText.text = _vo.did;
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
    }
    private void OnCloseExpandPanel()
    {
        m_expandPanel.SetActive(false);
    }
    private void UpdateUIDText(string _uid)
    {
        m_UIDText.text = _uid;
    }
    private void OnSetCurrentGroupName()
    {
        playerInfoVO.targetGroupName = m_targetGroupName.text;
        OnChangeCurrentGroupName();
    }
    public void UpdateGroupNameText(string _groupName)
    {
        m_groupNameText.text = _groupName;
    }
    public void UpdateHandDeviceStatus(PlayerStatus _status)
    {
        switch (_status)
        {
            case PlayerStatus.Unknown:
                m_handDeviceIndicator.color = Color.grey;
                break;
            case PlayerStatus.Connected:
                m_handDeviceIndicator.color = Color.cyan;
                break;
            case PlayerStatus.Offline:
                m_handDeviceIndicator.color = Color.yellow;
                break;
        }
    }
}
