using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    [Header("Motorcycle")]
    [SerializeField] private GameObject motorcycle; // assign prefab in Inspector
    [SerializeField] private Transform spawnPoint; // assign empty Transform in Inspector

    public void CompleteTheft(GameObject theftPlayer, GameObject targetVehicle)
    {
        Debug.Log("Car theft successful! Player can now driving.");

        // Disable player
        var playerMove = theftPlayer.GetComponent<PlayerMovement>();
        if (playerMove != null) playerMove.enabled = false;
        theftPlayer.SetActive(false);

        //enable car movement
        var carMove = targetVehicle.GetComponent<CarMovement>();
        if (carMove != null) carMove.enabled = true;

    }

    public void FailTheft(GameObject theftPlayer)
    {
        Debug.Log("Car theft failed - get a motorbike instead");

        // disable player
        var playerMove = theftPlayer.GetComponent<PlayerMovement>();
        if (playerMove != null) playerMove.enabled = false;
        theftPlayer.SetActive(false);

        // spawn motorcycle
        if (motorcycle != null && spawnPoint != null)
        {
            GameObject moto = Instantiate(motorcycle, spawnPoint.position, Quaternion.identity);

            var motoMove = moto.GetComponent<CarMovement>();
            if (motoMove != null)
            {
                motoMove.enabled = true;
                motoMove.moveSpeed = 3f; // slower than car
            }

            // CarExitManager can also be used for motorcycles
        }
        else
        {
            Debug.LogWarning("motorcycle prefab or spawn point not assigned");
        }
    }
}

}