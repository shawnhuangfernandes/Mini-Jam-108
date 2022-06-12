// Author:  Joseph Crump
// Date:    06/10/22

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Component that listens to events from the <see cref="PlayerInput"/>
/// component and makes the inputs easy to interface from other scripts.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerInput playerInput;

    /// <summary>
    /// Player input action to "Skip" the rock.
    /// </summary>
    public VirtualButton Skip = new VirtualButton();

    private void OnEnable()
    {
        // The registration is delayed until end of frame because the input
        // component does some internal things OnEnable()

        ScriptUtility.DoAtEndOfFrame(RegisterInputEvents);
    }

    private void OnDisable()
    {
        UnregisterInputEvents();
    }

    private void OnValidate()
    {
        playerInput ??= GetComponent<PlayerInput>();
    }

    private void RegisterInputEvents()
    {
        RegisterButton("Skip", Skip);
    }

    private void UnregisterInputEvents()
    {
        UnregisterButton("Skip", Skip);
    }

    private void RegisterButton(string name, VirtualButton button)
    {
        playerInput.actions[name].performed += button.OnInput;
        playerInput.actions[name].canceled += button.OnInput;
    }

    private void UnregisterButton(string name, VirtualButton button)
    {
        playerInput.actions[name].performed -= button.OnInput;
        playerInput.actions[name].canceled -= button.OnInput;
    }
}

/// <summary>
/// Class representing a button input by the player.
/// </summary>
public class VirtualButton
{
    /// <summary>
    /// Event raised whenever the button is pressed down.
    /// </summary>
    public event Action Pressed;

    /// <summary>
    /// Event raised whenever the button is released.
    /// </summary>
    public event Action Released;

    /// <summary>
    /// Returns true the frame that this input was pressed.
    /// </summary>
    public bool wasPressed { get; private set; }

    /// <summary>
    /// Returns true the frame that this input was released.
    /// </summary>
    public bool wasReleased { get; private set; }

    /// <summary>
    /// Returns true if the button is currently held down.
    /// </summary>
    public bool isDown { get; private set; }

    public virtual void OnInput(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (context.performed)
        {
            wasPressed = true;
            isDown = true;
            Pressed?.Invoke();

            ScriptUtility.DoAtEndOfFrame(() => wasPressed = false);
        }

        if (context.canceled)
        {
            wasReleased = true;
            isDown = false;
            Released?.Invoke();

            ScriptUtility.DoAtEndOfFrame(() => wasReleased = false);
        }
    }
}
