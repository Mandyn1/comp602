using UnityEngine;

public class CarMenu : MonoBehaviour
{
    [SerializeField] private GameObject carMenuUI;

    public void StealCar()
    {
        carMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Inspect() //not sure if needed
    {
        carMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Cancel()
    {
        carMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
