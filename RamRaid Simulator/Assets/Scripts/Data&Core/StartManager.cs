using UnityEngine;

public class StartManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameState>().GamePrep();
    }
}
