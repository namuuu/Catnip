using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Movement Settings")]
    [SerializeField]
    [Tooltip("The speed of the player when not sprinting.")]
    private float speed = 5f;
    [SerializeField]
    [Tooltip("The maximum speed of the player when sprinting.")]
    private float maxSpeed = 7.5f; // Maximum speed when sprinting
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 70f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
        HandleFacing();
        rb.linearVelocity = nextVelocity;
    }

    void OnEnable()
    {
        InputPolling.OnInteract += HandleInteract;
    }

    void OnDisable()
    {
        InputPolling.OnInteract -= HandleInteract;
    }

    private Vector2 desiredVelocity; // Desired velocity
    private Vector2 nextVelocity; // Next velocity

    private void HandleMovement()
    {
        Vector2 currentVelocity = rb.linearVelocity;

        // Calculate the desired velocity based on input
        desiredVelocity = InputPolling.movement * (InputPolling.sprint ? maxSpeed : speed);

        // Calculate x velocity
        nextVelocity.x = Mathf.MoveTowards(
            currentVelocity.x,
            desiredVelocity.x,
            Time.fixedDeltaTime * (Mathf.Abs(desiredVelocity.x) > Mathf.Abs(currentVelocity.x) ? acceleration : deceleration)
        );

        // Calculate y velocity
        nextVelocity.y = Mathf.MoveTowards(
            currentVelocity.y,
            desiredVelocity.y,
            Time.fixedDeltaTime * (Mathf.Abs(desiredVelocity.y) > Mathf.Abs(currentVelocity.y) ? acceleration : deceleration)
        );
    }

    private void HandleFacing()
    {
        // Character rotation
        if (nextVelocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (nextVelocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleInteract()
    {
        // Handle interaction logic here
        Debug.Log("Interacting with the environment.");
        string itemName = SlotController.instance.GetActiveItem();

        // TODO: Remove later, this is for testing purposes
        itemName = "Wheat:Age_0";

        PlantationController.instance.CreatePlantation(itemName, 0, 0);
    }
}
