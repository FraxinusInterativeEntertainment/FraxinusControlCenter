using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine.UI;

public class SideBarViewMediator : Mediator, IMediator
{
    public const string NAME = "SideBarViewMediator";

    protected SideBarView m_sideBarView { get { return m_viewComponent as SideBarView; } }

    public SideBarViewMediator(SideBarView _view) : base(NAME, _view)
    {
        m_sideBarView.GameStatusToggleChanged += OnGameStatusToggleChanged;
        m_sideBarView.QuestToggleChanged += OnQuestToggleChanged;
        m_sideBarView.ConditionToggleChanged += OnConditionToggleChanged;
        m_sideBarView.McuToggleChanged += OnMcuToggleChanged;
    }

    public override System.Collections.Generic.IList<string> ListNotificationInterests()
    {
        return new List<string>()
        {

        };
    }

    public override void HandleNotification(INotification notification)
    {
        string name = notification.Name;
        object vo = notification.Body;

        switch (name)
        {
           
        }
    }

    private void OnGameStatusToggleChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.GAME_STATUS_FORM);
        }
        else
        {
            _toggle.interactable = true;
        }
    }

    private void OnQuestToggleChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.QUEST_FORM);
        }
        else
        {
            _toggle.interactable = true;
        }
    }

    private void OnConditionToggleChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.CONDITION_FORM);
        }
        else
        {
            _toggle.interactable = true;
        }
    }

    private void OnMcuToggleChanged(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            _toggle.interactable = false;
            SendNotification(Const.Notification.SHOW_MAIN_PANEL_CONTENT, Const.UIFormNames.MCU_FORM);
        }
        else
        {
            _toggle.interactable = true;
        }
    }

}
