using UnityEditor.SceneManagement;
using UnityEngine;

public class BasicMinigameController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject menuCanvas;
    private PlayerMovement player;

    public LootableItem currentTargetItem;
    public PlayerWallet playerWallet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set closed by default
        menuCanvas.SetActive(false);
        if (menuCanvas == null)
        {
            Debug.LogError("Menu Canvas is not assigned!");
        }

        //get pl;aye to freeze movement
        player = playerObj.GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("PlayerMovement component not found on player object!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Steal()
    {
        //player chooses to steal, so gains a point
        //TODO: Yahya works in this section
        Debug.Log($"[Steal] Start. item={(currentTargetItem ? currentTargetItem.name : "NULL")} " +
                  $"wallet={(playerWallet ? playerWallet.name : "NULL")}");

        int payout = LootSystem.TryStealAndPay(currentTargetItem, playerWallet);

        Debug.Log($"[Steal] payout={payout} stolen={(currentTargetItem && currentTargetItem.IsStolen)} " +
                  $"balanceAfter={(playerWallet ? playerWallet.Balance : -1)}");

        Close();
    }

    public void Leave()
    {
        //player chooses to not steal so do not gain point
        //TODO: Yahya works in this section
        Debug.Log("Player chose to leave without stealing.");
        Close();
    }

    public void Open()
    {
        //open the menu and freeze player
        menuCanvas.SetActive(true);
        player.Freeze();
    }

    public void Close()
    {
        //open the menu and unfreeze player
        menuCanvas.SetActive(false);
        player.UnFreeze();
    }
}
