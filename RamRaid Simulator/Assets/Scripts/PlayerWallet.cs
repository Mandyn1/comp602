using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public float currentBalance = 0f;

    public void AddMoney(float amount)
    {
        currentBalance += amount;
        Debug.Log("Money added: $" + amount + " | New Balance: $" + currentBalance);
    }
}

