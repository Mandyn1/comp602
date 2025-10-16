using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartingRoles : MonoBehaviour
{
    public string hostRole;
    public string otherRole;
    public GameObject player1RoleText;
    public GameObject player2RoleText;

    public GameObject randomiseButton;
    public GameObject manualButton;
    public GameObject swapButton;

    public void saveRoles()
    {
        hostRole = player1RoleText.GetComponent<TMP_Text>().text;
        otherRole = player2RoleText.GetComponent<TMP_Text>().text;
    }
    
    public void swapRoles()
    {
        string temp = player1RoleText.GetComponent<TMP_Text>().text;
        player1RoleText.GetComponent<TMP_Text>().text = player2RoleText.GetComponent<TMP_Text>().text;
        player2RoleText.GetComponent<TMP_Text>().text = temp;
    }

    public void randomiseRoles()
    {
        swapButton.SetActive(false);
        player1RoleText.SetActive(false);
        player2RoleText.SetActive(true);
        randomiseButton.SetActive(false);
        manualButton.SetActive(true);

        int randomValue = Random.Range(1, 20);
        if (randomValue % 2 == 0)
        {
            player1RoleText.GetComponent<TMP_Text>().text = "Raider";
            player2RoleText.GetComponent<TMP_Text>().text = "Police";
        }
        else
        {
            player1RoleText.GetComponent<TMP_Text>().text = "Police";
            player2RoleText.GetComponent<TMP_Text>().text = "Raider";
        }

    }

    public void manualRoles()
    {
        player1RoleText.SetActive(true);
        player2RoleText.SetActive(true);
        randomiseButton.SetActive(true);
        manualButton.SetActive(false);
        swapButton.SetActive(true);
    }
}
