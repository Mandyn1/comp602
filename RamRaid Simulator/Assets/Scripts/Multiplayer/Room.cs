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
        GameObject.Find("ServerManager").GetComponent<CreateJoinRoom>().JoinRoom(Name.text);
    }
    
}
