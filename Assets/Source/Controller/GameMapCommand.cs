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
        string name = _notification.Name;

        switch (name)
        {
            case Const.Notification.RECV_PLAYER_POS_INFOS:
                List<PlayerPosInfo> playerPosInfos = (obj as PlayerPosInfosVO).playerPosInfos;

               for (int i = 0; i < playerPosInfos.Count; i++)
                {
                    string hsv = GetHsvByPosition(gameMapProxy.GetHsvRefMap(), playerPosInfos[i].position);
                    string roomId = gameMapProxy.GetRoomIdByHsv(hsv);

                    Debug.Log(playerPosInfos[i].tagId + ": " + roomId + " (" + hsv + ")");
                    //UpdatePlayerInfos & roomID
                }
                break;
        }

    }

    private string GetHsvByPosition(Texture2D _hsvRefMap, Coor2D position)
    {
        string hsvStr = null;
        float h;
        float s;
        float v;

        int x = (int)(position.x * (_hsvRefMap.width - 1));
        int y = (int)(position.y * (_hsvRefMap.height - 1));

        Color.RGBToHSV(_hsvRefMap.GetPixel(x, y), out h, out s, out v);

        if (v < MIN_V)
        {
           //TODO: Remove comment after testing 
           //return null;
        }

        hsvStr = Mathf.RoundToInt(h * 100).ToString() + "_" + Mathf.RoundToInt(s * 100).ToString()+ "_" + Mathf.RoundToInt(v * 100).ToString();

        return hsvStr;
    }
}
