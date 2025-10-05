using UnityEngine;

public class MakePersistant : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsByType<PlayerData>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);


    }
}
