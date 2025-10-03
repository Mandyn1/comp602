using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //variables
    [SerializeField] private float moveSpeed = 5f;
    private Boolean freeze = false;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //update players position
        if (!freeze)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        //move
        if (!freeze)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void Freeze()
    {
        //freeze players movement
        freeze = true;
    }

    public void UnFreeze()
    {
        //unfreezes players movement
        freeze = false;
    }

}
