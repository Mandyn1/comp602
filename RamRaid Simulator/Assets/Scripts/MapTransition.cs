using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    //instance variables
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    [SerializeField] float teleportDistance = 2f;
    enum Direction { Up, Down, Left, Right };

    private void Awake()
    {
        //set the confiner variable to the Cinemachine confiner componment from CmCam
        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with the collision box
        if (collision.gameObject.CompareTag("Player"))
        {
            //change the confining field for the camera to the new entered area 
            confiner.BoundingShape2D = mapBoundry;
            updatePlayerPosition(collision.gameObject);
        }
    }

    private void updatePlayerPosition(GameObject player)
    {
        //get the players position 
        Vector3 newPos = player.transform.position;

        //use switch statement to "teleport" the player past the other waypoint so no interference 
        switch (direction)
        {
            case Direction.Up:
                newPos.y += teleportDistance;
                break;
            case Direction.Down:
                newPos.y -= teleportDistance;
                break;
            case Direction.Left:
                newPos.x += teleportDistance;
                break;
            case Direction.Right:
                newPos.x -= teleportDistance;
                break;
        }

        //update the position
        player.transform.position = newPos;
    }
}