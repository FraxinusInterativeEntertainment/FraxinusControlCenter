using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapBeacon : MapBeacon
{
    [SerializeField]
    private Text m_uidText;
    [SerializeField]
    private Image m_beaconImage;

    public void Init(PlayerInfo _playerInfo)
    {
        ShowInfo(true);

        m_uidText.text = _playerInfo.uid;
        m_beaconImage.color = Color.yellow;
    }

    public void ShowInfo(bool _value)
    {
        m_uidText.gameObject.SetActive(_value);
    }
}
