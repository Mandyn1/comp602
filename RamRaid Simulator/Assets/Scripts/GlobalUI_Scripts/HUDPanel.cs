using UnityEngine;
using TMPro;

public class HUDPanel : MonoBehaviour
{
    [Header("UI Refs")]
    public TextMeshProUGUI roleNameText;
    public TextMeshProUGUI scoreText;
    public GameObject      balanceGroup;   // assign ONLY on the left/local panel
    public TextMeshProUGUI balanceText;

    public bool isPlayerPanel = false;
    public bool isOpponentPanel = false;

    string _playerName = "";
    PlayerRole _role = PlayerRole.Raider;

    public void Clear()
    {
        if (roleNameText) roleNameText.text = "";
        if (scoreText) scoreText.text = "";
        if (balanceText) balanceText.text = "";
    }
    void Awake() => Clear(); // Wipe Placeholder text on Start
    
    public void SetIdentity(string playerName, PlayerRole role)
    {
        _playerName = playerName;
        _role = role;
        if (roleNameText)
            roleNameText.text = $"{(role == PlayerRole.Police ? "Police" : "Raider")}: {playerName}";
    }

    public void SetScore(int score)
    {
        if (scoreText)
            scoreText.text = $"Current Score: {score:n0}";
    }

    public void ShowBalance(bool show)
    {
        if (balanceGroup) balanceGroup.SetActive(show);
    }

    public void SetBalance(int dollars)
    {
        if (balanceText)
            balanceText.text = $"Balance: ${dollars:n0}";
    }
}
