using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Button Settings")]
    public InputActionProperty selectButton; // Property to hold the input action for the button

    // UnityEvents to be called when the button is pressed or released
    [Header("Button Events")]
    public UnityEvent onButtonPressed; // Event triggered when button is pressed
    public UnityEvent onButtonReleased; // Event triggered when button is released
    public UnityEvent onButtonHeld; // Event triggered while button is held

    private Coroutine buttonHoldCoroutine; // Reference to the Coroutine

    // Enable input actions when this script is enabled
    private void OnEnable()
    {
        selectButton.action.Enable(); // Enable the button action
        selectButton.action.started += OnButtonPressed; // Register the callback for button press start
        selectButton.action.canceled += OnButtonReleased; // Register the callback for button release
    }

    // Disable input actions when this script is disabled
    private void OnDisable()
    {
        selectButton.action.Disable(); // Disable the button action
        selectButton.action.started -= OnButtonPressed; // Unregister the callback for button press start
        selectButton.action.canceled -= OnButtonReleased; // Unregister the callback for button release
    }

    // Callback function for when the button is pressed
    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        onButtonPressed?.Invoke(); // Invoke the UnityEvent (if it's not null)

        // Start the Coroutine to handle the "held" state
        if (buttonHoldCoroutine == null)
        {
            buttonHoldCoroutine = StartCoroutine(ButtonHeldCoroutine());
        }
    }

    // Callback function for when the button is released
    private void OnButtonReleased(InputAction.CallbackContext context)
    {        
        onButtonReleased?.Invoke(); // Invoke the UnityEvent (if it's not null)

        // Stop the Coroutine when the button is released
        if (buttonHoldCoroutine != null)
        {
            StopCoroutine(buttonHoldCoroutine);
            buttonHoldCoroutine = null;
        }
    }

    // Coroutine that runs while the button is held down
    private IEnumerator ButtonHeldCoroutine()
    {
        while (selectButton.action.ReadValue<float>() > 0) // While button is held
        {
            // Log or perform continuous actions while button is held down
            onButtonHeld?.Invoke(); // Invoke the UnityEvent (if it's not null)

            // Optionally add a delay or perform actions every frame
            yield return null; // Wait for the next frame (or use a delay if needed, e.g., `yield return new WaitForSeconds(0.1f);`)
        }
    }
}
