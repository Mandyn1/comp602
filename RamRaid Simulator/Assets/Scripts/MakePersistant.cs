using UnityEngine;

public class MakePersistant : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsByType<MakePersistant>(FindObjectsSortMode.None).Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);


    }
}
