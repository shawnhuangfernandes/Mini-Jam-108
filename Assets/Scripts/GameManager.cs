// Author:  Shawn Huang Fernandes & Joseph Crump
// Date:    06/10/22

using UnityEngine;

/// <summary>
/// Game state manager.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game States")]
    [SerializeField]
    private GameState menuState;

    [SerializeField]
    private MenuToPlayTransitionState menutoPlayTransitionState;

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
            TargetState = menutoPlayTransitionState,
            Condition = () => InputHandler.Skip.wasReleased
        });

        menutoPlayTransitionState.AddTransition(new Transition()
        {
            TargetState = playState,
            Condition = () => menutoPlayTransitionState.SequenceFinished
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
