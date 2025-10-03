using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //variables 
    [SerializeField] public string transitionTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box and input string scene is set
        if (collision.gameObject.CompareTag("Player") && !string.IsNullOrEmpty(transitionTo))
        {
            //transition to scene
            SceneManager.LoadScene(transitionTo);
        }
        else
        {
            //print err log (change to usefull error eventually)
            Debug.Log("Some weird ass err, wrong scene name");
        }
    }
}
