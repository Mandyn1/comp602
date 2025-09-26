using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;
    [SerializeField] private CarMenu carMenu;        // reference to CarMenu script
    [TextArea] public string carInfo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //if the player is near car menu activate
        {
            carMenuUI.SetActive(true);
            carMenu.SetCar(carInfo);
        }
    }
}
