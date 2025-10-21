using Unity.VisualScripting;
using UnityEngine;
public class CarExitManager : MonoBehaviour
{
    //variables
    private static bool hasEntered = false;
    private static CarExitManager instance;

    [SerializeField] private float xCarSpawn;
    [SerializeField] private float yCarSpawn;

    void Start()
    {
        
        //when the scene will be loaded next they will need to be exiting, so need to:
        //- change car to be moving down
        //- spawn the car just outside the shop
        if (hasEntered)
        {
            //get the car object and the movement script
            GameObject car = GameObject.FindWithTag("Player");


            if (car != null)
            {
                CarMovement movement = car.GetComponent<CarMovement>();
                if (movement != null)
                {
                    //moify the spawn value 
                    movement.xSpawn = xCarSpawn;
                    movement.ySpawn = yCarSpawn;

                    //modify the position of which the car with travel
                    movement.moveInput = Vector2.down;
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player has collided with the box, this means they have entered the building
        hasEntered = true;

    }
}
