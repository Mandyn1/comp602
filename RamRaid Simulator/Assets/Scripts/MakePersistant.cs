using UnityEngine;

public class MakePersistant : MonoBehaviour
{
    // Keeps first instance of game object between scenes and destroys duplicates
    void Awake()
    {
        if (FindObjectsByType<MakePersistant>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);


    }
}
