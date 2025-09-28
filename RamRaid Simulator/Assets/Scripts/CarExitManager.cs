using Unity.VisualScripting;
using UnityEngine;
public class CarExitManager : MonoBehaviour
{
    //variables
    private static bool hasEntered = false;
    private static CarExitManager instance;

    [SerializeField] private float xCarSpawn;
    [SerializeField] private float yCarSpawn;

    void Awake()
    {
        //to keep state of entering / exiting after switching scenes

        //first check if already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }

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
            // must be reversed again to account for additional raids - josh
            hasEntered = false;

            GameObject.Find("GameManager").GetComponent<SendEvents>().FinishedRaidEvent();
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player has collided with the box, this means they have entered the building
        hasEntered = true;

    }
}
