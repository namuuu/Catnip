using UnityEngine.InputSystem; 
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float acceleration = 50f;
    public float deceleration =  70f; 
    public PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
        rb.linearVelocity = v2; 
    }

    private Vector2 v1; // Desired velocity
    private Vector2 v2; // Next velocity

    void HandleMovement()
    {
        // Sprint 
        playerInput = GetComponent<PlayerInput>();
        if (playerInput.actions["Sprint"].IsPressed())
        {
            speed = 7.5f; // Sprint speed
        }
        else
        {
            speed = 5f; // Normal speed
        }

        // Speed and acceleration
        Vector2 currentVelocity = rb.linearVelocity; 
        v1 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;

        v2.x = Mathf.MoveTowards(
            currentVelocity.x, 
            v1.x, 
            Time.fixedDeltaTime * (Mathf.Abs(v1.x) > Mathf.Abs(currentVelocity.x) ? acceleration : deceleration)
        );

        v2.y = Mathf.MoveTowards(
            currentVelocity.y, 
            v1.y, 
            Time.fixedDeltaTime * (Mathf.Abs(v1.y) > Mathf.Abs(currentVelocity.y) ? acceleration : deceleration)
        );

        // Character rotation
        if (v2.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (v2.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
