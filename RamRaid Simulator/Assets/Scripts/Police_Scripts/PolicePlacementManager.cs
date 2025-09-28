using System.Collections.Generic;
using UnityEngine;

public class PolicePlacementManager : MonoBehaviour
{
    // this is the camera, needs to be dragged in the inspector
    public Camera cam;

    // this is the police prefab, drag the prefab here
    public GameObject policePrefab;

    // number of police units allowed
    public int maxUnits = 3;

    // keeps track of the police units that get placed
    public List<GameObject> placedUnits = new List<GameObject>();

    public bool stopped = false;

    void Update()
    {
        if (!stopped)
        {
            // if left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                // check if we can still place more police units
                if (placedUnits.Count < maxUnits)
                {
                    // shoot a ray from the mouse into the world
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    // check if the ray hits something
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        // spawn police a little above the hit point so it doesn’t clip
                        Vector3 spawnPos = hit.point + Vector3.up * 0.5f;

                        // make the police unit appear
                        GameObject unit = Instantiate(policePrefab, spawnPos, Quaternion.identity);
                        unit.name = "PoliceCar" + (placedUnits.Count + 1).ToString();

                        // add it to the list
                        placedUnits.Add(unit);
                    }
                }
            }

            // if right mouse button is clicked
            if (Input.GetMouseButtonDown(1))
            {
                // check if there are any units to remove
                if (placedUnits.Count > 0)
                {
                    // get the last one
                    GameObject last = placedUnits[placedUnits.Count - 1];

                    // remove it from the list
                    placedUnits.RemoveAt(placedUnits.Count - 1);

                    // delete it from the game
                    Destroy(last);
                }
            }
        }
        
    }

    public void Stop()
    {
        stopped = true;
    }
}
