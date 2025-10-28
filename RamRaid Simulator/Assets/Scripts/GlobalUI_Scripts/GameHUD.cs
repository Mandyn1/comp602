using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance { get; private set; }

    [Header("Panels (Player = local)")]
    [SerializeField] private HUDPanel playerPanel;     // ALWAYS local player (left in layout)
    [SerializeField] private HUDPanel opponentPanel;   // ALWAYS opponent    (right in layout)

    void OnValidate()
    {
        // If fields are empty in the Inspector, try to auto-assign by flags.
        if (playerPanel == null || opponentPanel == null)
        {
            var panels = GetComponentsInChildren<HUDPanel>(true);
            foreach (var p in panels)
            {
                if (p.isPlayerPanel)   playerPanel   = p;
                if (p.isOpponentPanel) opponentPanel = p;
            }
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        // Safety: if still not assigned, try again at runtime
        if (playerPanel == null || opponentPanel == null) OnValidate();

        // Clear placeholder text immediately
        playerPanel?.Clear();
        opponentPanel?.Clear();

        // Local sees wallet; opponent does not
        playerPanel?.ShowBalance(true);
        opponentPanel?.ShowBalance(false);
    }

    // ---- identity ----
    public void Initialize(string localName, PlayerRole localRole,
                           string opponentName, PlayerRole opponentRole)
    {
        SetLocalIdentity(localName, localRole);
        SetOpponentIdentity(opponentName, opponentRole);
    }

    public void SetLocalIdentity(string name, PlayerRole role)      => playerPanel?.SetIdentity(name, role);
    public void SetOpponentIdentity(string name, PlayerRole role)   => opponentPanel?.SetIdentity(name, role);

    // ---- updates ----
    public void UpdateLocalScore(int score)       => playerPanel?.SetScore(score);
    public void UpdateOpponentScore(int score)    => opponentPanel?.SetScore(score);
    public void UpdateLocalBalance(int dollars)   => playerPanel?.SetBalance(dollars);
}
