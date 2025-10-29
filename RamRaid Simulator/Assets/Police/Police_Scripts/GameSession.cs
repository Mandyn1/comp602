using System.Collections.Generic;
using UnityEngine;
// NOT IN USE ANYMORE. EVERYTHING ELSE IS WORKING SO THIS IS DISABLED. DONT WANT TO BREAK ANYTHING.




// this script stays alive between scenes and remembers car positions
public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    // remembers placed car positions between scenes
    public List<Vector3> savedCarPositions = new List<Vector3>();

    // flag that tells GameLoop whether to show placement or raid layer
    public bool goRaid = false;

    void Awake()
    {
        // make sure there's only one GameSession and it survives scene loads
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // called before leaving the placement scene
    public void SaveCars(List<GameObject> cars)
    {
        savedCarPositions.Clear();

        foreach (var c in cars)
        {
            if (c == null) continue;
            savedCarPositions.Add(c.transform.position);
        }

        Debug.Log($"Saved {savedCarPositions.Count} car positions.");
    }
}
