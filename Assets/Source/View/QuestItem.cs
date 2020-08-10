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
    private Text m_capacityText;
    [SerializeField]
    private Text m_lengthText;

    private QuestControlView m_uestControlView;

    private void Start()
    {
        m_questButton.onClick.AddListener(() => { OpenQuestInfo();  }) ;
    }
    public QuestItem Init(GroupInfoVO _vo)
    {
        SetName(_vo.name);
        SetCapacity(_vo.capacity);
        SetLength(_vo.length);
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
    public void SetCapacity(float _capacity)
    {
        m_capacityText.text = _capacity.ToString();
    }
    public void SetLength(float _length)
    {
        m_lengthText.text = _length.ToString();
    }
    private void OpenQuestInfo()
    {
        m_uestControlView.GetGroupName(m_groupNameText.text);
        
    }
}
