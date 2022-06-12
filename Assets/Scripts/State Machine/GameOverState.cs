// Author:  Joseph Crump
// Date:    06/10/22

using Cinemachine;
using UnityEngine;

/// <summary>
/// Gamestate for the 'game over' segment.
/// </summary>
public class GameOverState : GameState
{
    [SerializeField]
    [Tooltip("The camera for the game end.")]
    private CinemachineVirtualCamera gameOverCamera;

    [SerializeField]
    [Tooltip("The UI display on game end.")]
    private GameObject gameOverUI;


    [SerializeField]
    [Tooltip("The points scored per distance traveled")]
    private float PointsPerDistance;


    [SerializeField]
    [Tooltip("The points scored per successful skip.")]
    private float PointsPerSkip;

    private RockController _player;
    private RockController player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<RockController>();

            return _player;
        }
    }

    protected override void OnStateEnter()
    {
        PlayerProperty.Points.Value += PlayerProperty.Distance.Value * PointsPerDistance + PlayerProperty.Skips.Value * PointsPerSkip;
        PlayerProperty.TotalPoints.Value += PlayerProperty.Points.Value;

        gameOverUI.SetActive(true);
        player.ResetController();
        player.enabled = false;
    }

    protected override void OnStateExit()
    {
        gameOverUI.SetActive(false);
        gameOverCamera.m_Priority = 0;
    }
}
