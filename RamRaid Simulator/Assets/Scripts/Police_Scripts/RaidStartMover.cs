using UnityEngine;
using System.Collections.Generic;

// move nearest placed police car to a single raidLocation
public class RaidStartMover : MonoBehaviour
{
    public Transform raidLocation;                // drag RaidLocation here
    public PolicePlacementManager placementManager; // drag  PoliceManager here

    public float moveSpeed = 10f;                 // tweak if slow


    GameObject movingUnit;                        // the car we move

    // button calls this
    public void StartRaid()
    {
        if (!placementManager) { Debug.Log("no placementManager"); return; }
        if (!raidLocation)     { Debug.Log("no raidLocation"); return; }

        List<GameObject> cars = placementManager.placedUnits;
        if (cars == null || cars.Count == 0) { Debug.Log("no cars placed"); return; }

        // pick closest car

        movingUnit = FindNearest(cars, raidLocation.position);
        Debug.Log("moving: " + movingUnit?.name);
    }

    void FixedUpdate()
    {
        if (!movingUnit || !raidLocation) return;

        // straight line move toward raid point
        movingUnit.transform.position = Vector3.MoveTowards(
         movingUnit.transform.position,
        raidLocation.position,
        moveSpeed * Time.fixedDeltaTime
        );

        // stop when close
        if (Vector3.Distance(movingUnit.transform.position, raidLocation.position) < 0.1f)
        {
        Debug.Log("arrived");
        movingUnit = null;
        }
    }

    // helper: nearest car
    static GameObject FindNearest(List<GameObject> list, Vector3 to)
    {
        GameObject best = null; float bestD = Mathf.Infinity;
        foreach (var g in list)
        {
        float d = Vector3.Distance(g.transform.position, to);
        if (d < bestD) { bestD = d; best = g; }
        }
        return best;
    }
}
