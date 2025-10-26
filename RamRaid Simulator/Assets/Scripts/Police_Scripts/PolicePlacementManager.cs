using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// super simple placer + saver + loader 
public class PolicePlacementManager : MonoBehaviour
{
    public Camera cam;               
    public GameObject policePrefab;   
    public int maxUnits = 3;

    // other scripts read this 
    public List<GameObject> placedUnits = new List<GameObject>();

    bool stopped = false;

    // key for PlayerPrefs json
    const string SaveKey = "PolicePositionsV1";

    void Awake()
    {
        // keep this object when scenes change
        DontDestroyOnLoad(gameObject);

        // whenever a scene loads, auto spawn saved police there
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (stopped) return;

        // left click = place
        if (Input.GetMouseButtonDown(0) && placedUnits.Count < maxUnits)
        {
            if (cam == null || policePrefab == null) { Debug.LogWarning("Placer missing camera or prefab"); return; }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 pos = hit.point + Vector3.up * 0.5f; // tiny lift so it doesn't clip
                GameObject unit = Instantiate(policePrefab, pos, Quaternion.identity);
                unit.name = "PoliceCar" + (placedUnits.Count + 1);
                placedUnits.Add(unit);
            }
        }

        // right click = undo last
        if (Input.GetMouseButtonDown(1) && placedUnits.Count > 0)
        {
            var last = placedUnits[placedUnits.Count - 1];
            placedUnits.RemoveAt(placedUnits.Count - 1);
            if (last != null) Destroy(last);
        }
    }

    // call this from Start Raid button (first item)
    public void Stop()
    {
        stopped = true;

        // pack positions
        var bag = new V3Bag();
        foreach (var u in placedUnits)
            if (u != null) bag.list.Add(new V3(u.transform.position));

        // save json
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(bag));
        PlayerPrefs.Save();

        Debug.Log($"[PolicePlacementManager] Saved {bag.list.Count} police positions.");
    }

    // auto spawn saved cars whenever a scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey(SaveKey)) return;

        var json = PlayerPrefs.GetString(SaveKey, "");
        if (string.IsNullOrEmpty(json)) return;

        var bag = JsonUtility.FromJson<V3Bag>(json);
        if (bag?.list == null || policePrefab == null) return;

        int spawned = 0;
        foreach (var v in bag.list)
        {
            Instantiate(policePrefab, v.ToV3(), Quaternion.identity);
            spawned++;
        }
        Debug.Log($"[PolicePlacementManager] Loaded & spawned {spawned} police units in '{scene.name}'.");
    }

    
    [System.Serializable] class V3 { public float x, y, z; public V3() { } public V3(Vector3 v) { x = v.x; y = v.y; z = v.z; } public Vector3 ToV3() => new Vector3(x, y, z); }
    [System.Serializable] class V3Bag { public List<V3> list = new List<V3>(); }
}

