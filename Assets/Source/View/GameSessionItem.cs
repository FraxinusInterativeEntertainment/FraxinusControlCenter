using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameSessionItem : MonoBehaviour
{
    [SerializeField]
    private Text m_gameIdText;
    [SerializeField]
    private Text m_gameTimeText;
    [SerializeField]
    private Text m_playerNumberText;
    
    public void SetSessionInfo(GameSessionInfo _vo)
    {
        m_gameIdText.text = _vo.game_id;
        m_gameTimeText.text = _vo.game_time;
    }
}
