using UnityEngine;

public class CarMenu : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;

    public void StealCar()
    {
        carMenuUI.SetActive(false);

    }
    public void Inspect() //not sure if needed
    {
        carMenuUI.SetActive(false);

    }
    public void Cancel()
    {
        carMenuUI.SetActive(false);

    }
}
