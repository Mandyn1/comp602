using UnityEngine;
using System;

public class LocationModifier : MonoBehaviour
{
    [Header("Identity (for future UI)")]
    public string locationId = "default_id";
    public string displayName = "Unnamed Location";

    [Header("Rewards / Difficulty")]
    [Tooltip("Additional multiplier applied to all item payouts in this location.")]
    [Min(0f)] public float rewardMultiplier = 1f;

    [Tooltip("For later: use to scale minigame difficulty, patrol density, etc.")]
    [Range(0, 5)] public int difficultyTier = 1;

    [Header("Lifecycle")]
    [Tooltip("If true, this location becomes the active one on Awake().")]
    public bool makeActiveOnAwake = true;

    // --- Static access for the rest of the game ---
    public static LocationModifier Active { get; private set; }
    public static float ActiveRewardMultiplier => Active ? Mathf.Max(0f, Active.rewardMultiplier) : 1f;

    public static event Action<LocationModifier> OnActiveLocationChanged;

    private void Awake()
    {
        if (makeActiveOnAwake) Activate();
    }

    /// <summary>Make this the active location (used by LootSystem and UI).</summary>
    public void Activate()
    {
        if (Active != null && Active != this)
        {
            Debug.LogWarning($"[LocationModifier] Replacing active location '{Active.displayName}' with '{displayName}'.");
        }
        Active = this;
        OnActiveLocationChanged?.Invoke(this);
        Debug.Log($"[LocationModifier] Active location: {displayName} (x{rewardMultiplier})");
    }

    private void OnDestroy()
    {
        if (Active == this)
        {
            Active = null;
            OnActiveLocationChanged?.Invoke(null);
        }
    }
}
