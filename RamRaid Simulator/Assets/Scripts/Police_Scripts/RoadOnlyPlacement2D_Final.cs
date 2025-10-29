using UnityEngine;
using System.Collections.Generic;

// this script is for placing and removing police cars only on road areas
// it checks the road image alpha (transparent = not a road)
// and shows a red X if you can’t place there, green check when you can
public class RoadOnlyPlacement2D_Final : MonoBehaviour
{
    [Header("scene stuff ")]
    public Camera cam;                          // main camera (should be orthographic PLS REMEMBER)
    public SpriteRenderer roadMaskSR;           // the sprite renderer that has the road overlay image
    public BoxCollider2D roadMaskCollider;      // the box collider that covers the entire map area
    public GameObject policeCarPrefab;          // prefab for the police car 
    public Transform parentForCars;             // keep hierarchy tidy(dont need to use)


    [Header("road texture (the one with transparency)")]
    [Tooltip("assign your original Road.png (Texture Type = Default, Read/Write = ON)")]
    public Texture2D maskTexture;               // this is the readable texture used to check alpha (for road mask png)

    // rules !
    [Header("placement rules")]
    [Range(0f, 1f)] public float alphaThreshold = 0.10f; // how solid/opaque the pixel must be to count as road
    public float minSpacing = 1.5f;                      // min distance between two cars
    public float minRaidDistance = 12f;                  // min distance from raid locations
    public List<Transform> raidLocations;                // raid location objects here

    //debug / testing
    [Header("debug and testing toggles")]
    public bool bypassRoadCheck = false;                 // ignore road mask 
    public bool bypassSpacingCheck = false;              // ignore spacing 
    public bool bypassRaidCheck = false;                 // ignore raid distances 
    public bool logDebug = false;                        // prints debug messages to console (hopefully this isnt broken)

   
    [Header("placement indicator stuff")]
    public SpriteRenderer indicatorSR;                   // the small sprite that follows the mouse
    public Sprite validSprite;                           // green check 
    public Sprite invalidSprite;                         // red x
    public bool hideWhenValid = false;                   // if true, only show red X (no green)
    public float indicatorScale = 1f;                    // how big the indicator looks (0.25 is ideal)

    private Texture2D _maskTex;                          // internal reference to the texture itll actually read

    void Start()
    {
        if (cam == null) cam = Camera.main;

        if (roadMaskSR == null || roadMaskSR.sprite == null)
        {
        Debug.LogError("pls assign the RoadMask SpriteRenderer");
        enabled = false; return;
        }

        if (roadMaskCollider == null)
        {
        Debug.LogError("pls assign the RoadMask BoxCollider2D.");
        enabled = false; return;
        }


        _maskTex = maskTexture != null ? maskTexture : roadMaskSR.sprite.texture;

        if (_maskTex == null)
        {
        Debug.LogError("no road mask texture found.");
        enabled = false; return;
        }

        if (!_maskTex.isReadable)
        Debug.LogWarning("mask texture isn’t readable .. theres a read/write setting");

        // turn off indicator at start so it doesn’t show on load
        if (indicatorSR != null)
        indicatorSR.enabled = false;
    }
    void Update()
    {
        UpdateIndicator();          // moveand recolor the red X/green indicator
        if (Input.GetMouseButtonDown(0)) TryPlaceCar();   // left click to place
        if (Input.GetMouseButtonDown(1)) TryRemoveCar();  // right click to remove
    }

   // car placer
    void TryPlaceCar()
    {
        // talk to the manager script so we know max cars etc
        var mgr = FindObjectOfType<PolicePlacementManager>();
        if (mgr != null && !mgr.CanPlaceMore())
        {
        Log("reached max units.");
            return;
        }

        // get the mouse position in world coords + uv on texture
        if (!TryGetMouseWorld(out Vector2 world2D, out float u, out float v))
            return;

        // check if it’s allowed to place here
        if (!ValidateAt(world2D, u, v, out string reason))
        {
        Log(reason);
        return;
        }

        // if valid, spawn the police car
        GameObject go = Instantiate(policeCarPrefab, new Vector3(world2D.x, world2D.y, 0f), Quaternion.identity, parentForCars);

        // register the car so the manager + distance scripts can track it
        if (mgr != null)
            mgr.RegisterUnit(go);

        Log("car placed successfully!");
    }

// remove a car
    void TryRemoveCar()
    {
        var mgr = FindObjectOfType<PolicePlacementManager>();
        if (mgr == null) return;

        if (!TryGetMouseWorld(out Vector2 world2D, out _, out _))
        return;

        // first: check for any car under the cursor directly
        var hits = Physics2D.OverlapPointAll(world2D);
        foreach (var h in hits)
        {
        if (h == null) continue;
            var marker = h.GetComponentInParent<PoliceCarMarker>(); // this tag marks “real” cars
            if (marker != null)
            {
             mgr.RemoveUnit(marker.gameObject);
            Log("removed car under cursor.");
                return;
            }
        }

        // if no direct hit, remove nearest one in small radius
        mgr.RemoveNearest(world2D, 1.2f);
        Log("removed nearest car (if within radius).");
    }

//update indicator
    void UpdateIndicator()
    {
        if (indicatorSR == null) return;

        // convert mouse to world
        if (!TryGetMouseWorld(out Vector2 world2D, out float u, out float v))
        {
        indicatorSR.enabled = false;
        return;
        }

        // move indicator to cursor pos
        indicatorSR.transform.position = new Vector3(world2D.x, world2D.y, 0f);
        indicatorSR.transform.localScale = Vector3.one * indicatorScale;

        // check if spot is valid
        bool valid = ValidateAt(world2D, u, v, out _);

        // handle colors + visibility
        if (valid)
        {
        if (hideWhenValid)
            {
            indicatorSR.enabled = false;
            return;
            }
            indicatorSR.enabled = true;
            if (validSprite != null) indicatorSR.sprite = validSprite;
            indicatorSR.color = new Color(0f, 1f, 0f, 0.8f);  // green = good
        }
        else
        {
        indicatorSR.enabled = true;
        if (invalidSprite != null) indicatorSR.sprite = invalidSprite;
        indicatorSR.color = new Color(1f, 0f, 0f, 0.8f);  // red = blocked
        }
    }
//validation
    bool ValidateAt(Vector2 world2D, float u, float v, out string reason)
    {
        reason = "";

        // check if inside map
        if (!roadMaskCollider.OverlapPoint(world2D))
        {
        reason = "click inside map area!";
        return false;
        }

        // road alpha check
        if (!bypassRoadCheck)
        {
            if (_maskTex == null || !_maskTex.isReadable)
            {
             reason = "mask texture not readable.";
            return false;
            }

            // convert uv to pixel
            Rect tr = roadMaskSR.sprite.textureRect;
            int px = Mathf.Clamp(Mathf.RoundToInt(tr.x + u * tr.width), 0, _maskTex.width - 1);
            int py = Mathf.Clamp(Mathf.RoundToInt(tr.y + v * tr.height), 0, _maskTex.height - 1);

            float alpha = _maskTex.GetPixel(px, py).a;
            if (alpha < alphaThreshold)
            {
            reason = $"not on a road (alpha {alpha:0.00} < {alphaThreshold:0.00})";
            return false;
            }
        }

        // spacing (only block if near another real car)
        if (!bypassSpacingCheck)
        {
            var hits = Physics2D.OverlapCircleAll(world2D, minSpacing);
            foreach (var h in hits)
            {
             if (h == null) continue;
            if (h == roadMaskCollider) continue;  // ignore the road collider itself
            if (h.GetComponentInParent<PoliceCarMarker>() != null)
                {
                    reason = "too close to another car.";
                    return false;
                }
            }
        }

        // raid distance
        if (!bypassRaidCheck && raidLocations != null)
        {
            foreach (var raid in raidLocations)
            {
                if (raid == null) continue;
                if (Vector2.Distance(world2D, raid.position) < minRaidDistance)
                {
                reason = "too close to raid location.";
                return false;
                }
          }
        }

        return true; // if all checks pass, valid spot
    }

// helpers :3
    bool TryGetMouseWorld(out Vector2 world2D, out float u, out float v)
    {
        world2D = Vector2.zero; u = v = 0f;

        // convert mouse to world pos
        Vector3 m = Input.mousePosition;
        float zDist = Mathf.Abs(cam.transform.position.z - roadMaskSR.transform.position.z);
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(m.x, m.y, zDist));
        world2D = new Vector2(world.x, world.y);

        // if outside map, return false
        if (!roadMaskCollider.OverlapPoint(world2D))
        return false;

        // calculate UV (used for alpha check)
        Bounds b = roadMaskSR.bounds;
        u = Mathf.Clamp01(Mathf.InverseLerp(b.min.x, b.max.x, world2D.x));
        v = Mathf.Clamp01(Mathf.InverseLerp(b.min.y, b.max.y, world2D.y));
        return true;
    }

    void Log(string msg) { if (logDebug) Debug.Log($"[placement] {msg}"); }
    void LogErr(string msg) { Debug.LogError($"[placement] {msg}"); }
}
