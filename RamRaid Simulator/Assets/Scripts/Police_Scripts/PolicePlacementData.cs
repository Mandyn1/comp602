using System.Collections.Generic;
using UnityEngine;

// saves and loads the positions of police units between scenes
public static class PolicePlacementData
{
    // store all positions from the placement scene
    public static List<Vector3> savedPositions = new List<Vector3>();

    // called when leaving Police_S1_CarPlacer
    public static void Save(List<GameObject> units)
    {
        savedPositions.Clear();
        foreach (var u in units)
            savedPositions.Add(u.transform.position);
        Debug.Log($"Saved {savedPositions.Count} police units.");
    }

    // called when loading another scene
    public static List<Vector3> Load()
    {
        Debug.Log($"Loaded {savedPositions.Count} police units.");
        return savedPositions;
    }
}
