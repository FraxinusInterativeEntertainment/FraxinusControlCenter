using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPosSImulator : MonoBehaviour
{
    public List<PlayerPosInfoSim> playerPosInfos;

    public const float dTime = 1;
    public float nextTime;
    public bool sendInfos = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + dTime;

            if (sendInfos)
                AppFacade.instance.SendNotification(Const.Notification.RECV_PLAYER_POS_INFOS, PackPlayerPosInfos(playerPosInfos));
        }
    }

    private PlayerPosInfosVO PackPlayerPosInfos(List<PlayerPosInfoSim> _infos)
    {
        PlayerPosInfosVO vo = new PlayerPosInfosVO();

        for (int i = 0; i < _infos.Count; i++)
        {
            vo.playerPosInfos.Add(new PlayerPosInfo(_infos[i].TagId, new Coor2D(_infos[i].x, _infos[i].y)));
        }

        return vo;
    }
}


[System.Serializable]
public class PlayerPosInfoSim
{
    public string TagId;
    [Range(0, 1)]
    public float x;
    [Range(0, 1)]
    public float y;

}
