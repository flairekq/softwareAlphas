using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text roomName;
    [SerializeField] Text playerCountText;

    RoomInfo info;
    public void SetUp(RoomInfo _info)
    {
        info = _info;
        roomName.text = info.Name;
        playerCountText.text = info.PlayerCount.ToString() + "/4 players";
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }
}
