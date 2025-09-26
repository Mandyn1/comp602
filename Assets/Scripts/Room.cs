using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    public TMP_Text Name;

    public void JoinRoom()
    {
        GameObject.Find("CreateJoinRoom").GetComponent<CreateJoinRoom>().JoinRoom(Name.text);
    }
    
}
