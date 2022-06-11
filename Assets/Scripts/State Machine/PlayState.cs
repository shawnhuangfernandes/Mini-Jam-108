// Author:  Joseph Crump
// Date:    06/10/22

using Cinemachine;
using System.Collections;
using UnityEngine;

/// <summary>
/// Gamestate for the main gameplay segment.
/// </summary>
public class PlayState : GameState
{
    [SerializeField]
    [Tooltip("The player follow camera view for gameplay.")]
    private CinemachineVirtualCamera playmodeCamera;

    [SerializeField] 
    [Tooltip("The UI display on during rock skipping.")]
    private GameObject playmodeUI;

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
        playmodeUI.SetActive(true);
        player.enabled = true;
        player.ResetController();
        playmodeCamera.m_Priority = 1;
    }

    protected override void OnStateUpdate()
    {
        PlayerProperty.Distance.Value += player.LateralSpeed * Time.deltaTime;
    }

    protected override void OnStateExit()
    {
        playmodeUI.SetActive(false);
        playmodeCamera.m_Priority = 0;
    }
}
