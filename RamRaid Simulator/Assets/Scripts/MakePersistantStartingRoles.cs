using UnityEngine;

public class MakePersistantStartingRoles : MonoBehaviour
{
    // Keeps first instance of game object between scenes and destroys duplicates
    void Awake()
    {
        if (FindObjectsByType<MakePersistantStartingRoles>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);


    }
}
