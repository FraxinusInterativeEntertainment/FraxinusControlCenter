using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPosSImulator : MonoBehaviour
{
    public static PlayerPosSImulator instance;
    public List<PlayerPosInfoSim> playerPosInfos;

    public float dTime = 0.5f;
    public float nextTime;
    public bool sendInfos = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + dTime;

            if (sendInfos)
                AppFacade.instance.SendNotification(Const.Notification.RECV_PLAYER_POS_INFOS, playerPosInfos);
        }
    }

    public void OnNewVirtualPlayer(string _tagId)
    {
        playerPosInfos.Add(new PlayerPosInfoSim(_tagId));
    }
}


[System.Serializable]
public class PlayerPosInfoSim
{
    public string TagId;
    [Range(0, 100)]
    public float x;
    [Range(0, 100)]
    public float y;

    public PlayerPosInfoSim(string _tagId)
    {
        TagId = _tagId;
    }

}
