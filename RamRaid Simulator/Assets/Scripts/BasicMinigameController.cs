using JetBrains.Annotations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BasicMinigameController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject menuCanvas;
    public GameObject counterObj;
    public GameObject attachedItem;
    private PlayerMovement player;
    private PointCounter counter;

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

        //get the point counter in this scene
        counter = counterObj.GetComponent<PointCounter>();
        if (counter == null)
        {
            Debug.LogError("point counter is null");
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
        Debug.Log("player chooses to steal!!!");
        this.Close();

        //increment the counter for the player to gain a point
        counter.Increment();
        attachedItem.SetActive(false);
    }

    public void Leave()
    {
        //player chooses to not steal so do not gain point
        //TODO: Yahya works in this section
        Debug.Log("player chooses to not steal");
        this.Close();
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
