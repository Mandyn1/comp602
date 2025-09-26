using UnityEngine;

public static class LootSystem
{
    /// Pays the player for stealing the item (if valid & not already stolen).
    /// Returns the amount paid (0 if not paid).

    public static int TryStealAndPay(LootableItem item, PlayerWallet wallet)
    {
        if (item == null || wallet == null) return 0;
        if (item.IsStolen) return 0;

        int payout = Mathf.Max(0, item.GetPayout());
        if (payout <= 0) return 0;

        wallet.AddMoney(payout);
        item.MarkStolen();
        return payout;
    }
}
