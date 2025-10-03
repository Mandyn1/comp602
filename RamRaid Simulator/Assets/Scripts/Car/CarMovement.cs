using UnityEngine;

public class CarMovement : MonoBehaviour
{
    //variables
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float xSpawn;
    [SerializeField] public float ySpawn;
    private Rigidbody2D rb;
    [SerializeField] public Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //set the spawn position
        Vector2 pos = new Vector2(xSpawn, ySpawn);
        rb.position = pos;
    }


    void FixedUpdate()
    {
        //continuelsly move the car upwards to "ram" into the building

        //check first if init has been done before moving
        rb.linearVelocity = moveInput * moveSpeed;

        
    }
}
