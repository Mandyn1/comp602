using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public GameObject car;     
    public GameObject motorbike; 
    public Transform spawnPoint; //vehicle spawns

    void Start()
    {
        string vehicle = PlayerPrefs.GetString("Vehicle", "Motorbike");

        if (vehicle == "Car")
            Instantiate(car, spawnPoint.position, Quaternion.identity);
        else
            Instantiate(motorbike, spawnPoint.position, Quaternion.identity);
    }
}
