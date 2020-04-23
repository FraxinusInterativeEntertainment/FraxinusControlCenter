using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class GameMapCommand : SimpleCommand
{
    public const float MIN_V = 25.0f;

    public override void Execute(INotification _notification)
    {
        object obj = _notification.Body;
        GameMapProxy gameMapProxy;
        gameMapProxy = Facade.RetrieveProxy(GameMapProxy.NAME) as GameMapProxy;

        PlayerInfoProxy playerInfoProxy;
        playerInfoProxy = Facade.RetrieveProxy(PlayerInfoProxy.NAME) as PlayerInfoProxy;

        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.RECV_PLAYER_POS_INFOS:

                RawPosDataHandler(obj as List<PlayerPosInfoSim>, gameMapProxy, playerInfoProxy);
                break;
        }

    }

    private void RawPosDataHandler(List<PlayerPosInfoSim> _playerPosInfos, GameMapProxy _gameMapProxy, PlayerInfoProxy _playerInfoProxy)
    {
        if (_playerPosInfos == null || _playerPosInfos.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _playerPosInfos.Count; i++)
        {
            string hsv = GetHsvByPosition(_gameMapProxy.GetHsvRefMap(), new Coor2D(_playerPosInfos[i].x, _playerPosInfos[i].y));
            string deviceId = _playerPosInfos[i].TagId;
            PlayerInfo playerInfo = _playerInfoProxy.GetPlayerInfoByTagId(deviceId);

            if (playerInfo == null) 
            {
                continue;
            }

            playerInfo.posInfo.x = _playerPosInfos[i].x;
            playerInfo.posInfo.y = _playerPosInfos[i].y;
            playerInfo.posInfo.rid = _gameMapProxy.GetRoomIdByHsv(hsv);

            Debug.Log(playerInfo.posInfo.did + ": " + playerInfo.posInfo.rid + " (" + hsv + ")");
        }
        //TODO: Send PlayerInfo Updated notification
    }

    private string GetHsvByPosition(Texture2D _hsvRefMap, Coor2D position)
    {
        string hsvStr = null;
        float h;
        float s;
        float v;

        //TODO: 需要实地大小和地图大小的等比例换算
        int x = (int)(position.x * (_hsvRefMap.width - 1));
        int y = (int)(position.y * (_hsvRefMap.height - 1));

        Color.RGBToHSV(_hsvRefMap.GetPixel(x, y), out h, out s, out v);

        if (v < MIN_V)
        {
           //TODO: Remove comment after testing 
           //此处判断hsv的v是否低于有效阈值，如果低于，说明该像素是被压缩造成失真，应当被忽略。
           //return null;
        }

        hsvStr = Mathf.RoundToInt(h * 100).ToString() + "_" + 
                 Mathf.RoundToInt(s * 100).ToString()+ "_" + 
                 Mathf.RoundToInt(v * 100).ToString();

        return hsvStr;
    }
}
