
using UnityEditor.SceneManagement;

using UnityEngine;

public class BasicMinigameController : MonoBehaviour
{
    //TODO
    //[SerializeField] newScene;
    SceneTransition sceneTrans;
    public GameObject playerObj;
    public GameObject menuCanvas;
    private PlayerMovement player;

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
        Debug.Log("player chooses to steal!!!");
        this.Close();
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
