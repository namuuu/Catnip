using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float acceleration = 5f; 
    
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

    private Vector2 v1; //desired velocity
    private Vector2 v2; // next velocity

    void HandleMovement()
    {
        Vector2 currentVelocity = rb.linearVelocity;
        v1 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        v2.x = Mathf.MoveTowards(currentVelocity.x, v1.x, Time.fixedDeltaTime * acceleration);
        v2.y = Mathf.MoveTowards(currentVelocity.y, v1.y, Time.fixedDeltaTime * acceleration);
    }
}
