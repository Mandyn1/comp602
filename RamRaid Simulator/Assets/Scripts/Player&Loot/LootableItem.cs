using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LootableItem : MonoBehaviour
{
    [Header("Payout")]
    [Min(0)] public int baseValue = 100;
    [Tooltip("Final payout = baseValue * multiplier (rounded).")]
    [Min(0f)] public float multiplier = 1.0f;

    [Header("Rules")]
    [SerializeField] private bool canBeStolenOnce = true;

    public bool IsStolen { get; private set; }

    private Collider2D _collider;
    public GameObject managerObject;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider != null) _collider.isTrigger = true;
    }

    public int GetPayout()
    {
        float raw = Mathf.Max(0, baseValue) * Mathf.Max(0f, multiplier);
        return Mathf.RoundToInt(raw);
    }

    public void MarkStolen()
    {
        if (IsStolen) return;
        IsStolen = true;

        if (canBeStolenOnce && _collider) _collider.enabled = false;

        // Option A: hide the whole object after stealing
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (!collision.CompareTag("Player")) return;

        managerObject.GetComponent<RaidMinigameManager>().currentItem = gameObject;
        managerObject.GetComponent<RaidMinigameManager>().ShowMenu();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (baseValue < 0) baseValue = 0;
        if (multiplier < 0f) multiplier = 0f;
    }
#endif
}
