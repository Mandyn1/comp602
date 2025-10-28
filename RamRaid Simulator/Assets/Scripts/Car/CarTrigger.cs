using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;
    [SerializeField] private CarMenu carMenu;        // reference to CarMenu script
    public GameObject player;
    private PlayerMovement move;
    [TextArea] public string carInfo;

    void Start()
    {
        //get the players movement script
        if (player != null)
        {
            move = player.GetComponent<PlayerMovement>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //if the player is near car menu activate
        {
            //set the menus as active
            carMenuUI.SetActive(true);
            carMenu.SetCar(carInfo);

            //freeze the player because the menu UI is open
            if (move != null)
            {
                move.Freeze();
            }
        }
    }
}
