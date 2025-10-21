using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //variables 
    [SerializeField] private GameObject transitionTo;
    [SerializeField] private GameObject currentScene;
    bool wayPointActive = true;

    void Start()
    {
        //need to disable the parsed in scene first so no obverlap
        if (transitionTo != null)
        {
            transitionTo.SetActive(false);
        }
        else
        {
            //prinmt debiug
            Debug.Log("cannot set object as inacive (already null)");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box and input string scene is set
        if (collision.gameObject.CompareTag("Player") && wayPointActive)
        {
            //enable indoor object
            if (transitionTo != null)
            {
                transitionTo.SetActive(true); //enable the indoor raid object

                if (transitionTo.activeInHierarchy)
                {
                    currentScene.SetActive(false);
                }

                wayPointActive = false;
                
            }
            else
            {
                //give debug err
                Debug.Log("Object to load is null");
            }

            
        }
        else
        {
            //print err log (change to usefull error eventually)
            Debug.Log("Some weird ass err, wrong scene name");
        }
    }
}
