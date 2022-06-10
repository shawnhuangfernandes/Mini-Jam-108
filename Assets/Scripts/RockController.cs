// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom physics and input handler for the skipping rock.
/// </summary>
public class RockController : MonoBehaviour
{
    [Header("Properties")]
    [Tooltip("How rapidly the object accelerates towards the ground.")]
    public Stat Gravity = -9.8f;

    [SerializeField]
    [Tooltip("The plane on the Y-axis that is considered the surface level of the water.")]
    private float groundLevel = 0f;

    [Header("References")]
    [SerializeField]
    private InputHandler inputHandler;

    private void OnEnable()
    {
        inputHandler.Skip.Pressed += OnPlayerPressedSkipButton;
    }

    private void OnDisable()
    {
        inputHandler.Skip.Pressed -= OnPlayerPressedSkipButton;
    }

    private void OnPlayerPressedSkipButton()
    {
        Debug.Log("Skip!");
    }
}
