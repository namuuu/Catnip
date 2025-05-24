using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPolling : MonoBehaviour
{
    public PlayerInput playerInput;

    /// <summary>
    /// Event that is triggered when a number key (1-6) is pressed.
    /// </summary>
    public static Action<int> OnNumRowPressed;

    /// <summary>
    /// Event that is triggered when left click is pressed.
    /// </summary>
    public static Action OnInteract;

    /// <summary>
    /// Event that is triggered when the right click is pressed.
    /// summary>
    public static Action OnInspect;

    /// <summary>
    /// Whether the sprint action is currently pressed.
    /// </summary>
    public static bool sprint;

    /// <summary>
    /// The movement vector using the left stick on the controller.
    /// </summary>
    public static Vector2 movement;

    private void OnEnable()
    {
        playerInput.actions["1"].performed += OnNumRow;
        playerInput.actions["2"].performed += OnNumRow;
        playerInput.actions["3"].performed += OnNumRow;
        playerInput.actions["4"].performed += OnNumRow;
        playerInput.actions["5"].performed += OnNumRow;
        playerInput.actions["6"].performed += OnNumRow;

        playerInput.actions["Interact"].performed += context => OnInteract?.Invoke();
        playerInput.actions["Inspect"].performed += context => OnInspect?.Invoke();
    }

    private void OnDisable()
    {
        playerInput.actions["1"].performed -= OnNumRow;
        playerInput.actions["2"].performed -= OnNumRow;
        playerInput.actions["3"].performed -= OnNumRow;
        playerInput.actions["4"].performed -= OnNumRow;
        playerInput.actions["5"].performed -= OnNumRow;
        playerInput.actions["6"].performed -= OnNumRow;

        playerInput.actions["Interact"].performed -= context => OnInteract?.Invoke();
        playerInput.actions["Inspect"].performed -= context => OnInspect?.Invoke();
    }

    private void Update()
    {
        sprint = playerInput.actions["Sprint"].IsPressed();
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
    }


    private void OnNumRow(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        int slotIndex = context.action.name switch
        {
            "1" => 0,
            "2" => 1,
            "3" => 2,
            "4" => 3,
            "5" => 4,
            "6" => 5,
            _ => -1
        };

        if (slotIndex >= 0)
        {
            OnNumRowPressed?.Invoke(slotIndex);
        }
    }
}