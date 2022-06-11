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
        gameOverUI.SetActive(true);
        gameOverCamera.m_Priority = 1;
    }

    protected override void OnStateExit()
    {
        gameOverUI.SetActive(false);
        gameOverCamera.m_Priority = 0;
    }
}
