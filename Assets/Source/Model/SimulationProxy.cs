using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class SimulationProxy : Proxy, IProxy, IResponder
{
    public const string NAME = "SimulationProxy";

    public SimulationProxy() : base(NAME) { }

    public void SendLogin(object _data)
    {

    }

    public void OnResult(object _data)
    {

    }

    public void OnFault(object _data)
    {

    }

    public void AddVirtualPlayer(VirtualPlayerVO _vo)
    {
        WWWForm form = new WWWForm();
        form.AddField("device_id", _vo.device_id);
        form.AddField("user_id", _vo.user_id);
        form.AddField("game_id", _vo.game_id);

        HttpService addNewPlayer = new HttpService(Const.Url.ADD_USER_TO_GAME + "?" + "device_id=" + _vo.device_id
                                                                                    + "&user_id=" + _vo.user_id
                                                                                    + "&game_id=" + _vo.game_id,
                                                                                    HttpRequestType.Get);
        addNewPlayer.SendRequest<HttpResponse>((HttpResponse response) => { });
    }

    public void GenerateVirtualPlayer(string _gameId)
    {
        HttpService addNewPlayer = new HttpService(Const.Url.GET_TEST_USER + "?" + "game_id=" + _gameId, HttpRequestType.Get);
        addNewPlayer.SendRequest<VirtualPlayerResponse>(VirtualPlayerCallBack);
    }

    private void VirtualPlayerCallBack(VirtualPlayerResponse _response)
    {
        Debug.Log("AddPlayer result: " + _response.err_code + _response.band_id);

        if (_response.err_code != 0)
        {
            SendNotification(Const.Notification.WARNING_POPUP, _response.err_code + ": " + _response.err_msg);
        }
        
        SendNotification(Const.Notification.ADD_VIRTUAL_PLAYER_TO_GAME, new VirtualLoginVO(_response.band_id, "tag" + _response.band_id.Substring(_response.band_id.Length - 2, 2), ""));
    }

    public void VirtualPersonalDeviceLogin(VirtualLoginVO _vo)
    {
        WWWForm form = new WWWForm();
        form.AddField("band_id", _vo.bandId);
        form.AddField("device_id", _vo.tagId);

        HttpService m_httpService = new HttpService(Const.Url.BAND_ID_LOGIN, HttpRequestType.Post, form);
        m_httpService.SendRequest<HttpResponse>(VirtualLoginCallback);
    }

    private void VirtualLoginCallback(HttpResponse _response)
    {
        SendNotification(Const.Notification.DEBUG_LOG, _response.err_code + _response.err_msg);
        Debug.Log(_response.err_code + _response.err_msg);
    }
}

public class VirtualPlayerVO
{
    public string device_id { get; set; }
    public string user_id { get; set; }
    public string game_id { get; set; }

    public VirtualPlayerVO(string _did, string _uid, string _gid)
    {
        device_id = _did;
        user_id = _uid;
        game_id = _gid;
    }
}

public class VirtualPlayerResponse : HttpResponse
{ 
    public string nickname { get; set; }
    public string band_id { get; set; }

    public VirtualPlayerResponse(int _errCode, string _errMsg) : base (_errCode, _errMsg)
    { 
    
    }
}

public class VirtualLoginVO
{ 
    public string bandId { get; set; }
    public string tagId { get; set; }
    public string mofiId { get; set; }

    public VirtualLoginVO(string _bid, string _tid, string _mfid)
    {
        bandId = _bid;
        tagId = _tid;
        mofiId = _mfid;
    }
}