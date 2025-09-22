using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{
    // Object for generating new rooms on the room list
    public GameObject roomPreFab;

    // Generate room list from connected Photon Service
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            GameObject room = Instantiate(roomPreFab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            room.GetComponent<Room>().Name.text = roomList[i].Name;
        }
    }
}
