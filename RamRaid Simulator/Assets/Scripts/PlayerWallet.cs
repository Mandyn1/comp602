using UnityEngine;
using System;

public class PlayerWallet : MonoBehaviour
{
    [Min(0)][SerializeField] private int startingBalance = 0;

    [Header("Runtime (read-only)")]
    [SerializeField] private int balance = 0; // visible in Inspector during Play Mode
    public int Balance => balance;

    public event Action<int> OnBalanceChanged;

    private void Awake()
    {
        balance = Mathf.Max(0, startingBalance);
        OnBalanceChanged?.Invoke(balance);
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        balance += amount;
        OnBalanceChanged?.Invoke(balance);
    }

    public bool TrySpend(int amount)
    {
        if (amount < 0) return false;
        if (balance < amount) return false;
        balance -= amount;
        OnBalanceChanged?.Invoke(balance);
        return true;
    }
}
