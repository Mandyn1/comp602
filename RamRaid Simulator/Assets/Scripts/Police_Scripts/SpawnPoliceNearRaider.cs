using UnityEngine;

public class SpawnPoliceNearRaider : MonoBehaviour
{
    public GameObject policePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null || policePrefab == null)
            {
                Debug.LogWarning("Missing player tag or police prefab!");
                return;
            }

            // place car right beside and a bit above the player
            Vector3 spawnPos = player.transform.position 
                               + player.transform.right * 3f     // 3 units to the right
                               + Vector3.up * 1.5f;              // lifted 1.5 above ground

            GameObject police = Instantiate(policePrefab, spawnPos, Quaternion.identity);
            Debug.Log($"Police spawned at {spawnPos}");
        }
    }
}
