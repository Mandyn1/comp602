using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPreFab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            GameObject room = Instantiate(roomPreFab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            room.GetComponent<Room>().Name.text = roomList[i].Name;
        }
    }
}
