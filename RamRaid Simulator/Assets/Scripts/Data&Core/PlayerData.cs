using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerData : MonoBehaviour
{
    public int score;
    public int bank;
    public string position;

    public void ResetBank()
    {
        bank = 0;
    }
}
