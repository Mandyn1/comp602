using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidModeController : MonoBehaviour
{
    [Header("Roots to toggle")]
    public GameObject placementRoot;   // drag PlacementRoot
    public GameObject raidRoot;        // drag RaidLayer (keep disabled at start)

    [Header("Tags (instances must be tagged)")]
    public string carTag  = "PoliceCar";   // tag police prefab, so placed cars inherit it
    public string raidTag = "RaidTarget";  // tag  raid locations

    [Header("Movement")]
    public float moveSpeed = 2f;           // slower
    public float accel = 1.5f;             // smooth start

    [Header("Pathfinding")]
    public Texture2D roadMask;             // drag road mask (Read/Write ON in settings)
    public Vector2 worldMin = new Vector2(-45, -25);
    public Vector2 worldMax = new Vector2( 45,  25);

    bool clicked = false;

    // hook this to your Ready button OnClick
    public void StartRaid()
    {
        Debug.Log($"[Raid] StartRaid on {name} | placementRoot={(placementRoot ? placementRoot.name : "NULL")} | raidRoot={(raidRoot ? raidRoot.name : "NULL")}");

        if (clicked) return; clicked = true;

        // 1) turn off placement
        if (placementRoot)
        {
        foreach (var mb in placementRoot.GetComponentsInChildren<MonoBehaviour>(true))
        mb.enabled = false;
        placementRoot.SetActive(false);
        }

        // 2) turn on raid UI/layer
        if (raidRoot) raidRoot.SetActive(true);

        // 3) find cars & targets
        var cars = GameObject.FindGameObjectsWithTag(carTag);
        var targets = GameObject.FindGameObjectsWithTag(raidTag);

        if (cars.Length == 0) { Debug.LogWarning("[Raid] No PoliceCar instances. Tag your prefab 'PoliceCar'."); return; }
        if (targets.Length == 0) { Debug.LogWarning("[Raid] No RaidTarget objects. Tag your locations."); return; }

        Transform best = null;

        //This has changed to get the actual raid location and pick the correct car rather than the first car placed - Josh
        foreach (GameObject t in targets)
        {
            if (t.name == GameObject.Find("GameManager").GetComponent<GameState>().currentRaidLocation.ToString())
            {
                best = t.transform;
            }
        }

        // 4) pick first car + nearest target 
        GameObject car = cars[0];
        //Transform best = targets[0].transform;
        float bestD = Vector3.Distance(best.position, car.transform.position);
        for (int i = 1; i < targets.Length; i++)
        {
            float d = Vector3.Distance(best.position, cars[i].transform.position);
            if (d < bestD) { bestD = d; car = cars[i]; }
        }

        Debug.Log($"[Raid] sending {car.name} → {best.name} (dist {bestD:F1})");

        // 5) try roadmask path.. if none then go straight
        List<Vector3> path = null;
        if (roadMask != null)
        {
            var grid = new RoadGrid(roadMask, worldMin, worldMax);
            path = grid.FindPath(car.transform.position, best.position);
            Debug.Log("[Raid] path points = " + (path != null ? path.Count : 0));
        }

        if (path != null && path.Count > 0)
        StartCoroutine(FollowPath(car, path, moveSpeed, accel));
        else
        StartCoroutine(DriveStraight(car, best.position, moveSpeed, accel));

        // camer nudge (if needed)
        if (Camera.main) Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    IEnumerator FollowPath(GameObject car, List<Vector3> pts, float speed, float accel)
    {
        var mover = car.GetComponent<CarMover>();
        if (!mover) mover = car.AddComponent<CarMover>();
        mover.speed = speed;
        mover.acceleration = accel;

        for (int i = 0; i < pts.Count; i++)
        {
        mover.target = pts[i];
        mover.active = true;
        while (mover.active) yield return null;
        }
        Debug.Log("[Raid] arrived (path)");
    }

    IEnumerator DriveStraight(GameObject car, Vector3 target, float speed, float accel)
    {
        var mover = car.GetComponent<CarMover>();
        if (!mover) mover = car.AddComponent<CarMover>();
        mover.speed = speed;
        mover.acceleration = accel;
        mover.target = target;
        mover.active = true;

        while (mover.active) yield return null;
        Debug.Log("[Raid] arrived (straight)");
    }

    // (see bounds in scene view 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 a = new Vector3(worldMin.x, worldMin.y, 0);
        Vector3 b = new Vector3(worldMax.x, worldMin.y, 0);
        Vector3 c = new Vector3(worldMax.x, worldMax.y, 0);
        Vector3 d = new Vector3(worldMin.x, worldMax.y, 0);
        Gizmos.DrawLine(a, b); Gizmos.DrawLine(b, c); Gizmos.DrawLine(c, d); Gizmos.DrawLine(d, a);
    }
}
