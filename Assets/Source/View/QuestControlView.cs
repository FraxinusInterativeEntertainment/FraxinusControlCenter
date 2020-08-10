using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestControlView : UIViewBase
{
    public event Action RequestGroupName = delegate { };
    public event Action<Toggle> OnTimelineToggle = delegate { };
    public event Action<Toggle> OnQuestToggle = delegate { };
    public event Action<string> OnQuestInfo = delegate { };
    private readonly Dictionary<string, QuestItem> m_questItems = new Dictionary<string, QuestItem>();

    [SerializeField]
    private Toggle m_timelineToggle;
    [SerializeField]
    private Toggle m_questToggle;
    [SerializeField]
    private GameObject m_timelinePanel;
    [SerializeField]
    private QuestPanelView m_questPanel;
    [SerializeField]
    private GameObject m_questItemContainer;
    [SerializeField]
    private GameObject m_questItemPrefab;
    [SerializeField]
    private GameObject m_gameTimeItemContainer;
    [SerializeField]
    private GameObject m_gameTimePrefab;

    [SerializeField]
    private Text m_groupNameText;

    private void Awake()
    {
        AppFacade.instance.RegisterMediator(new QuestControlViewMediator(this));
    }
    void Start()
    {
        m_timelineToggle.onValueChanged.AddListener(delegate { OnTimelineToggle(m_timelineToggle); });
        m_questToggle.onValueChanged.AddListener((isOn) => { OnQuestToggle(m_questToggle); });
        OnTimelineToggle(m_timelineToggle);
    }
    void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(QuestControlViewMediator.NAME);
        gameTimes.Clear();
    }

    public override void Show()
    {
        base.Show();
        RequestGroupName();
        OnTimelineToggle(m_questToggle);
        //TODO: Remove after testing
        m_questPanel.UpdateQuestPanel("a");
    }
    public void UpdateAllGroupInfos(GroupInfoVO _vo)
    {
        if (!m_questItems.ContainsKey(_vo.name))
        {
            GameObject groupNameItem = Instantiate(m_questItemPrefab);
            groupNameItem.transform.SetParent(m_questItemContainer.transform);
            QuestItem questItem = groupNameItem.GetComponent<QuestItem>();
            questItem.InitView(this);
            questItem.SetName(_vo.name);
            questItem.SetLength(_vo.length);
            questItem.SetCapacity(_vo.capacity);
            m_questItems.Add(_vo.name, questItem);
        }
        else
        {
            m_questItems[_vo.name].SetName(_vo.name);
            m_questItems[_vo.name].SetLength(_vo.length);
            m_questItems[_vo.name].SetCapacity(_vo.capacity);
        }
    }
    Dictionary<string, GameTimeItem> gameTimes = new Dictionary<string, GameTimeItem>();
    
    public void UpdateGameTime(GroupInfoVO _groupInfo,Color _color)
    {
        if (!gameTimes.ContainsKey(_groupInfo.name))
        {
            GameObject gameTimeItem = Instantiate(m_gameTimePrefab);
            gameTimeItem.transform.SetParent(m_gameTimeItemContainer.transform);
            GameTimeItem timeItem = gameTimeItem.GetComponent<GameTimeItem>();
            gameTimes.Add(_groupInfo.name, timeItem);
            timeItem.SetItemInfos(_groupInfo, _color);
        }
    }
    public void UpdateGameTimeInfos(string _groupName,float _time)
    {
        if (gameTimes.ContainsKey(_groupName))
        {
            gameTimes[_groupName].CalculateTimeLine(_time);
        }
    }
    public void ShowTimelinePanel()
    {
        m_timelinePanel.SetActive(true);
        m_questPanel.gameObject.SetActive(false);
    }
    public void GetGroupName(string _name)
    {
        OnQuestToggle(m_timelineToggle);
        OnQuestInfo(_name);
    }
    public void ShowQuestInfoName(string _vo)
    {
        m_groupNameText.text = _vo;
    }
    public void ShowQuestPanel()
    {
        m_timelinePanel.SetActive(false);
        m_questPanel.gameObject.SetActive(true);
    }
}


