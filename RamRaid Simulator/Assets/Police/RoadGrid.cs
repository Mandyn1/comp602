using System.Collections.Generic;
using UnityEngine;

public class RoadGrid
{
    Texture2D mask;
    Vector2 worldMin, worldMax;
    int texW, texH;

    public float alphaThreshold = 0.4f; // road if alpha > this

    public RoadGrid(Texture2D roadMask, Vector2 min, Vector2 max)
    {
        mask = roadMask;
        worldMin = min;
        worldMax = max;
        texW = mask != null ? mask.width : 0;
        texH = mask != null ? mask.height : 0;
    }

    bool Inside(int x, int y) { return x >= 0 && y >= 0 && x < texW && y < texH; }

    bool Walkable(int x, int y)
    {
        if (!Inside(x, y) || mask == null) return false;
        // make sure mask is Read/Write enabled (Import Settings)
        return mask.GetPixel(x, y).a > alphaThreshold;
    }

    Vector2Int WorldToPix(Vector3 w)
    {
        int px = Mathf.Clamp(Mathf.RoundToInt(Mathf.InverseLerp(worldMin.x, worldMax.x, w.x) * (texW - 1)), 0, texW - 1);
        int py = Mathf.Clamp(Mathf.RoundToInt(Mathf.InverseLerp(worldMin.y, worldMax.y, w.y) * (texH - 1)), 0, texH - 1);
        return new Vector2Int(px, py);
    }

    Vector3 PixToWorld(int x, int y)
    {
        float wx = Mathf.Lerp(worldMin.x, worldMax.x, (float)x / (texW - 1));
        float wy = Mathf.Lerp(worldMin.y, worldMax.y, (float)y / (texH - 1));
        return new Vector3(wx, wy, 0);
    }

    Vector2Int NearestWalkable(Vector2Int p)
    {
        if (Walkable(p.x, p.y)) return p;
        // tiny expanding ring search
        for (int r = 1; r <= 32; r++)
        {
            for (int dx = -r; dx <= r; dx++)
            {
                int x1 = p.x + dx, y1 = p.y + r;
                int x2 = p.x + dx, y2 = p.y - r;
                if (Walkable(x1, y1)) return new Vector2Int(x1, y1);
                if (Walkable(x2, y2)) return new Vector2Int(x2, y2);
            }
            for (int dy = -r + 1; dy <= r - 1; dy++)
            {
                int x1 = p.x + r, y1 = p.y + dy;
                int x2 = p.x - r, y2 = p.y + dy;
                if (Walkable(x1, y1)) return new Vector2Int(x1, y1);
                if (Walkable(x2, y2)) return new Vector2Int(x2, y2);
            }
        }
        return p;
    }

    // very plain BFS (4-direction) → list of world waypoints
    public List<Vector3> FindPath(Vector3 startWorld, Vector3 goalWorld)
    {
        var s0 = WorldToPix(startWorld);
        var g0 = WorldToPix(goalWorld);
        var s = NearestWalkable(s0);
        var g = NearestWalkable(g0);

        var came = new Dictionary<Vector2Int, Vector2Int>();
        var q = new Queue<Vector2Int>();

        if (!Walkable(s.x, s.y) || !Walkable(g.x, g.y))
            return new List<Vector3>();

        q.Enqueue(s);
        came[s] = s;

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        bool found = false;
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            if (cur == g) { found = true; break; }
            for (int i = 0; i < 4; i++)
            {
                var nb = new Vector2Int(cur.x + dx[i], cur.y + dy[i]);
                if (!Inside(nb.x, nb.y)) continue;
                if (!Walkable(nb.x, nb.y)) continue;
                if (came.ContainsKey(nb)) continue;
                came[nb] = cur;
                q.Enqueue(nb);
            }
        }

        var world = new List<Vector3>();
        if (!found) return world;

        // reconstruct
        var p = g;
        while (p != s)
        {
            world.Add(PixToWorld(p.x, p.y));
            p = came[p];
        }
        world.Reverse();

        // small decimation to reduce jitter (every Nth point)
        int step = Mathf.Max(1, Mathf.RoundToInt(world.Count / 32f));
        if (world.Count > 0 && step > 1)
        {
            var slim = new List<Vector3>();
            for (int i = 0; i < world.Count; i += step) slim.Add(world[i]);
            slim.Add(world[world.Count - 1]);
            world = slim;
        }

        return world;
    }
}
