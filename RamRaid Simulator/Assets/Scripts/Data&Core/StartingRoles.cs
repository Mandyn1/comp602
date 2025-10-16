using TMPro;
using UnityEngine;

public class StartingRoles : MonoBehaviour
{
    public string hostRole;
    public string otherRole;
    public TMP_Text player1RoleText;
    public TMP_Text player2RoleText;

    public void saveRoles()
    {
        hostRole = player1RoleText.text;
        otherRole = player2RoleText.text;
    }
    
    public void swapRoles()
    {
        string temp = player1RoleText.text;
        player1RoleText.text = player2RoleText.text;
        player2RoleText.text = temp;
    }
}
