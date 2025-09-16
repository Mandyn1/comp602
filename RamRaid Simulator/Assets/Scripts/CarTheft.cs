using UnityEngine;
using UnityEngine.InputSystem;

public class CarTheft : MonoBehaviour
{
    private bool playerInCarZone = false; //check if player is close enough to trigger interaction
    private GameObject theftPlayer;
    private bool menuOpen = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //check if the object in zone has tagged as player
        {
            playerInCarZone = true;
            theftPlayer = other.gameObject;
            Debug.Log("Player entered car theft zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //check if player has left the zone
        {
            playerInCarZone = false;
            theftPlayer = null;
            menuOpen = false; 
            Debug.Log("Player exited car theft zone");
        }
    }

    void Start()
    {
        Debug.Log("Car theft is active");

    }

    void Update()
    {
        if (playerInCarZone&& !menuOpen &&Keyboard.current.mKey.wasPressedThisFrame)        
        {
            menuOpen = true;
            Debug.Log("Car theft menu opened");
        }

        if (menuOpen)
        {
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                InspectVehicle();     
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                StealVehicle(); // start minigame,win it and get the car

            }
            
           else if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                menuOpen = false;
                Debug.Log("Car theft menu closed");
            }
        }
    }

    void InspectVehicle()
    {
        Debug.Log("Inspecting vehicle...");
        // Add inspection logic here
    }

    void StealVehicle()
    {
        Debug.Log("Starting car theft minigame...");
        // Add car theft minigame logic here
    }

    void CompleteTheft()
    {
        Debug.Log("Car theft completed! You got the car.");
        // Add logic to give the player the car
        var theftPlayerController = theftPlayer.GetComponent<PlayerController>();

        if (theftPlayerController != null)
        {
            theftPlayerController.AcquireCar(); // Assuming PlayerController has a method to acquire a car
        }

        var playerMove = theftPlayer.GetComponent<PlayerMovement>();
        if (playerMove != null)
        {
            playerMove.enabled = false; // disable player movement script
        }

        theftPlayer.SetActive(false); // hide player character

        var carMove = GetComponent<CarMovement>();
        if (carMove != null)
        {
            carMove.enabled = true; // enable car movement script
        }
    }

    void FailTheft() //now have to find a scooter or bike to steal
    {
        Debug.Log("Car theft failed"); 
        // Add logic for failure scenario

    }
}


