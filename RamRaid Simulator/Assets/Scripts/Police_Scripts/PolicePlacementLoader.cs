using UnityEngine;
using System.Collections.Generic;

// recreates the placed police cars in any other scene
public class PolicePlacementLoader : MonoBehaviour
{
    public GameObject policePrefab; // drag the SAME prefab used by PolicePlacementManager

    void Awake()
    {
        // grab saved positions and spawn a police unit at each one
        List<Vector3> positions = PolicePlacementData.Load();
        foreach (var p in positions)
        {
            Instantiate(policePrefab, p, Quaternion.identity);
        }
        Debug.Log($"[Loader] spawned {positions.Count} police units");
    }
}
