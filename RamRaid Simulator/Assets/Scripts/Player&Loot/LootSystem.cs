using UnityEngine;

public static class LootSystem
{
    
    /// Pays the player for stealing the item (if valid & not already stolen),
    /// applying both the item multiplier and the active location multiplier.
    /// Returns the amount paid (0 if not paid).
    
    public static int TryStealAndPay(LootableItem item, PlayerWallet wallet)
    {
        if (item == null || wallet == null) return 0;
        if (item.IsStolen) return 0;

        // Combine the item payout with the active location multiplier
        float locMul = GameObject.Find("GameManager").GetComponent<GameState>().currentRaidModifier; // defaults to 1 if none
        float raw = Mathf.Max(0, item.baseValue) * Mathf.Max(0f, item.multiplier) * Mathf.Max(0f, locMul);
        int payout = Mathf.RoundToInt(raw);

        if (payout <= 0) return 0;

        wallet.AddMoney(payout);
        item.MarkStolen();
        return payout;
    }
}
