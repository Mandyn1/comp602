using UnityEngine;

public class CarTheftMiniGame : MonoBehaviour
{
    [SerializeField] private VehicleManager vehicleManager;

    public void StartMiniGame(GameObject theftPlayer, GameObject targetVehicle)
    {
        Debug.Log("starting car theft mini game");

        //placeholder 
        bool win = Random.value > 0.5f;

        if (win) vehicleManager.CompleteTheft(theftPlayer, targetVehicle);
        else vehicleManager.FailTheft(theftPlayer);
    }
}
