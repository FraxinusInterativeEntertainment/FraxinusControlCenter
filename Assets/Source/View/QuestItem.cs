using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{

    [SerializeField]
    private Button m_questButton;
    [SerializeField]
    private Text m_groupNameText;
    [SerializeField]
    private Text m_questNodeText;

    private QuestControlView m_uestControlView;

    private void Start()
    {
        m_questButton.onClick.AddListener(() => { OpenQuestInfo();  }) ;
    }
    public QuestItem Init(QuestVO _vo)
    {
        SetName(_vo.group_name);
        SetNodeName(_vo.quest_node_name);
        return this;
    }
    public void InitView(QuestControlView _view)
    {
        m_uestControlView = _view;
    }
  
    public void SetName(string _name)
    {
        m_groupNameText.text = _name;
    }
    public void SetNodeName(string _nodeName)
    {
        m_questNodeText.text = _nodeName;
    }
    private void OpenQuestInfo()
    {
        m_uestControlView.GetGroupName(m_groupNameText.text);
        
    }
}
