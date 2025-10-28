using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PolicePlacementManager : MonoBehaviour
{
    // camera (if you use it elsewhere)
    public Camera cam;

    // police prefab (kept for compatibility with your other code)
    public GameObject policePrefab;

    // number of police units allowed
    public int maxUnits = 3;

    // keeps track of the police units that get placed
    public List<GameObject> placedUnits = new List<GameObject>();

    public bool stopped = false;

    /// <summary>
    /// Call this from your placement click code BEFORE placing/removing.
    /// Example:
    ///   if (manager.ShouldIgnoreUIClick()) return;
    /// </summary>
    public bool ShouldIgnoreUIClick()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    public void Stop() { stopped = true; }

    public bool CanPlaceMore()
    {
        return !stopped && placedUnits.Count < maxUnits;
    }

    public void RegisterUnit(GameObject unit)
    {
        if (unit != null && !placedUnits.Contains(unit))
            placedUnits.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        if (unit == null) return;
        int i = placedUnits.IndexOf(unit);
        if (i >= 0) placedUnits.RemoveAt(i);
        Destroy(unit);
    }

    public void RemoveNearest(Vector2 worldPos, float maxRadius = 1.2f)
    {
        if (placedUnits.Count == 0) return;
        GameObject best = null;
        float bestDist = float.MaxValue;
        foreach (var u in placedUnits)
        {
            if (u == null) continue;
            float d = Vector2.Distance(worldPos, u.transform.position);
            if (d < bestDist) { bestDist = d; best = u; }
        }
        if (best != null && bestDist <= maxRadius) RemoveUnit(best);
    }
}
