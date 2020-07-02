using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapBeacon : MapBeacon
{
    [SerializeField]
    private Text m_uidText;
    [SerializeField]
    private Text m_nicknameText;

    public void Init(PlayerInfo _playerInfo)
    {
        SetUidVisible(false);
        SetNicknameVisible(false);

        m_uidText.text = _playerInfo.uid;
        m_nicknameText.text = _playerInfo.nickName;
        m_beaconImage.color = Color.grey;
    }

    public void SetUidVisible(bool _value)
    {
        m_uidText.gameObject.SetActive(_value);
    }

    public void SetNicknameVisible(bool _value)
    {
        m_nicknameText.gameObject.SetActive(_value);
    }
}
