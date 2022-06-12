// Author:  Joseph Crump
// Date:    06/10/22

using Cinemachine;
using System.Collections;
using UnityEngine;
using JC.Audio2D;

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

    private EnvironmentVisuals _environmentVisuals;
    private EnvironmentVisuals environmentVisuals
    {
        get
        {
            if (_environmentVisuals == null)
                _environmentVisuals = FindObjectOfType<EnvironmentVisuals>();

            return _environmentVisuals;
        }
    }

    [SerializeField]
    [Tooltip("One shot sound on menu start")]
    private SoundEffect StartOneShot;

    [SerializeField]
    [Tooltip("Main Menu Soundtrack")]
    private Soundtrack GameTrack;

    [SerializeField]
    private float TrackDelay;

    private float Timer;

    protected override void OnStateEnter()
    {
        PlayerProperty.Distance.Value = 0;
        PlayerProperty.Skips.Value = 0;

        playmodeUI.SetActive(true);
        player.enabled = true;
        player.ResetController();
        playmodeCamera.m_Priority = 1;

        environmentVisuals.SetScrollSpeed(player.LateralSpeed);

        StartOneShot.Play();
        StartCoroutine(DelayedTrack());
    }

    protected override void OnStateUpdate()
    {
        PlayerProperty.Distance.Add(player.LateralSpeed * Time.deltaTime);

        Timer += Time.deltaTime;

        if (Timer > 2F)
        {
            environmentVisuals.SpawnFloater();
            Timer =0F;
        }
    }

    protected override void OnStateExit()
    {
        playmodeUI.SetActive(false);
        playmodeCamera.m_Priority = 0;

        environmentVisuals.SetScrollSpeed(0F);

        GameTrack.Stop();

        if (PlayerProperty.Distance > PlayerProperty.BestDistance)
            PlayerProperty.BestDistance.Value = PlayerProperty.Distance.Value;

        PlayerProperty.TotalDistance.Value += PlayerProperty.Distance;
        PlayerProperty.TotalSkips.Value += PlayerProperty.Skips;
    }

    IEnumerator DelayedTrack()
    {
        yield return new WaitForSeconds(TrackDelay);
        GameTrack.Play();
    }
}
