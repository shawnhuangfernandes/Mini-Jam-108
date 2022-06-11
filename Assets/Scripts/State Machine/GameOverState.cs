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

    protected override void OnStateEnter()
    {
        PlayerProperty.Points.Value = PlayerProperty.Distance.Value + PlayerProperty.Skips.Value;
        gameOverUI.SetActive(true);


    }

    protected override void OnStateExit()
    {
        gameOverUI.SetActive(false);
        gameOverCamera.m_Priority = 0;
    }
}
