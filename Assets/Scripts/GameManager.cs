// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Game phase management
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game States")]
    [SerializeField]
    private GameState menuState;

    [SerializeField]
    private GameState playState;

    [SerializeField]
    private GameState gameOverState;

    /// <summary>
    /// The current active <see cref="GameState"/>.
    /// </summary>
    public GameState CurrentState { get; private set; }

    private InputHandler _inputHandler;
    private InputHandler InputHandler
    {
        get
        {
            if (_inputHandler == null)
                _inputHandler = FindObjectOfType<InputHandler>();

            return _inputHandler;
        }
    }

    private RockController _rockController;
    private RockController RockController
    {
        get
        {
            if (_rockController == null)
                _rockController = FindObjectOfType<RockController>();

            return _rockController;
        }
    }

    private void Awake()
    {
        RockController.enabled = false;

        menuState.AddTransition(new Transition()
        {
            TargetState = playState,
            Condition = () => InputHandler.Skip.wasReleased
        });

        playState.AddTransition(new Transition()
        {
            TargetState = gameOverState,
            Condition = () => RockController.HasSunk
        });

        gameOverState.AddTransition(new Transition()
        {
            TargetState = menuState,
            Condition = () => InputHandler.Skip.wasReleased
        });
    }

    public void Start()
    {
        ChangeState(menuState);
    }

    private void Update()
    {
        if (CurrentState.TryTransitions(out GameState state))
        {
            ChangeState(state);
        }
    }

    public void ChangeState(GameState state)
    {
        if (CurrentState != null)
            CurrentState.enabled = false;

        CurrentState = state;
        CurrentState.enabled = true;
    }
}
