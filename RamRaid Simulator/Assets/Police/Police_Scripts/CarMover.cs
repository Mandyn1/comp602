using UnityEngine;

public class CarMover : MonoBehaviour
{
    public Vector3 target;
    public float speed = 2f;          // slower base speed
    public float acceleration = 1.5f; // smooth start
    public bool active = false;

    float currentSpeed = 0f;

    void Update()
    {
        if (!active) return;

        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude < 0.05f)
        {
            active = false;
            return;
        }

        // face direction
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang - 90f);

        // accelerate up to speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceleration * Time.deltaTime);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            currentSpeed * Time.deltaTime
        );
    }
}
