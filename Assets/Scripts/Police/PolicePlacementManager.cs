using System.Collections.Generic;
using UnityEngine;

public class PolicePlacementManager : MonoBehaviour
{
    public Camera cam;                 // drag main camera here in inspector
    public GameObject policePrefab;    // drag PoliceUnit prefab here
    public int maxUnits = 3;           // maximum number of police units

    private List<GameObject> placedUnits = new List<GameObject>();

    void Update()
    {
        // Left click = place a unit
        if (Input.GetMouseButtonDown(0))
        {
            if (placedUnits.Count < maxUnits)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // spawn a little above ground so it's visible
                    Vector3 spawnPos = hit.point + Vector3.up * 0.5f;
                    GameObject unit = Instantiate(policePrefab, spawnPos, Quaternion.identity);
                    placedUnits.Add(unit);
                }
            }
        }

        // right click = remove the last placed unit
        if (Input.GetMouseButtonDown(1))
        {
            if (placedUnits.Count > 0)
            {
                GameObject last = placedUnits[placedUnits.Count - 1];
                placedUnits.RemoveAt(placedUnits.Count - 1);
                Destroy(last);
            }
        }
    }
}
