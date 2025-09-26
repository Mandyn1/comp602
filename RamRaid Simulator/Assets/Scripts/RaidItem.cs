using UnityEngine;

public class RaidItem : MonoBehaviour
{
    [Header("Item Settings")]
    public float valueMultiplier = 1.0f;  // Multiplier for this item
    public float baseValue = 100f;        // Base value of all items

    [Header("Optional FX")]
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Make sure player has the "Player" tag
        {
            float totalValue = baseValue * valueMultiplier;

            PlayerWallet wallet = collision.GetComponent<PlayerWallet>();
            if (wallet != null)
            {
                wallet.AddMoney(totalValue);
            }

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Remove item after pickup
        }
    }
}
