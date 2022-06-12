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

    [SerializeField]
    private PlayToGameOverTransitionState playToGameOverTransitionState;

    [SerializeField]
    private GameState postMortemState;

    [SerializeField]
    [Tooltip("How many rounds of gameplay there are before the game ends.")]
    private int rounds = 10;

    /// <summary>
    /// The 'Menu' gamestate.
    /// </summary>
    public GameState MenuState => menuState;

    /// <summary>
    /// The 'Play' gamestate.
    /// </summary>
    public GameState PlayState => playState;

    /// <summary>
    /// The current active <see cref="GameState"/>.
    /// </summary>
    public GameState CurrentState { get; private set; }

    /// <summary>
    /// The current round of gameplay.
    /// </summary>
    public int CurrentRound
    {
        get => (int)PlayerProperty.Rounds.Value;
        private set => PlayerProperty.Rounds.Value = value;
    }

    /// <summary>
    /// Number of rounds of gameplay before the game ends.
    /// </summary>
    public int TotalRounds => rounds;

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
            TargetState = playToGameOverTransitionState,
            Condition = () => RockController.HasSunk
        });

        playToGameOverTransitionState.AddTransition(new Transition()
        {
            TargetState = gameOverState,
            Condition = () => playToGameOverTransitionState.SequenceFinished
        });

        gameOverState.AddTransition(new Transition()
        {
            TargetState = menuState,
            Condition = () => InputHandler.Skip.wasReleased
        });
    }

    public void Start()
    {
        menuState.Exited += OnMenuStateExited;
        gameOverState.Entered += OnGameOverStateEntered;

        CurrentRound = 0;
        ChangeState(menuState);
    }

    private void Update()
    {
        if (CurrentState.TryTransitions(out GameState state))
        {
            ChangeState(state);
        }
    }

    private void ChangeState(GameState state)
    {
        if (CurrentState != null)
            CurrentState.enabled = false;

        CurrentState = state;
        CurrentState.enabled = true;
    }

    private void OnMenuStateExited()
    {
        CurrentRound++;
    }

    private void OnGameOverStateEntered()
    {
        OnRoundFinished();
    }

    private void OnRoundFinished()
    {
        if (CurrentRound == rounds)
        {
            HandleLastRoundFinished();
            return;
        }
    }

    private void HandleLastRoundFinished()
    {
        ChangeState(postMortemState);
    }
}
