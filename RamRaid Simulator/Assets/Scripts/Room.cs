using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    // Data for room identification
    public TMP_Text Name;

    // Connected to button press, joins selected (this) room and moves to waiting room
    public void JoinRoom()
    {
        GameObject.Find("CreateJoinRoom").GetComponent<CreateJoinRoom>().JoinRoom(Name.text);
        GameObject.Find("RoomStage").SetActive(false);
        GameObject.Find("WaitingStage").SetActive(true);
    }
    
}
