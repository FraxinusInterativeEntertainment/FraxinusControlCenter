using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UserInfoItemViewMediator : Mediator, IMediator
{
    public const string NAME = "UserInfoItemViewMediator";
    protected UserInfoItemView m_userInfoItemView { get { return m_viewComponent as UserInfoItemView; } }
    public UserInfoItemViewMediator(UserInfoItemView _view,string _name) : base(_name, _view)
    {
        _view.OnChangeCurrentGroupName += TrySendPlayerTargetGroupName;
        _view.OnRemovePlayer += TryRemovePlayerFromGroup;
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
    private void TrySendPlayerTargetGroupName(string _targetGroupName)
    {
        SendNotification(Const.Notification.TRY_CHANGE_PLAYE_GROUP, new PlayerInfoVO(m_userInfoItemView.playerInfo.uid, 
                                                                                    _targetGroupName));
    }
    private void TryRemovePlayerFromGroup(PlayerInfo _playerInfo)
    {
        SendNotification(Const.Notification.TRY_REMOVE_PLAYER_FROM_GROUP, _playerInfo.uid);
    }
}
