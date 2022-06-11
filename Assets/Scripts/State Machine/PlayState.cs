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

    [SerializeField]
    [Tooltip("The animator used to drive the transition into the play sequence")]
    private Animator Anim;

    [SerializeField]
    private float SequenceLength = 3F;

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

    private Coroutine StartSequence;

    protected override void OnStateEnter()
    {
        if (StartSequence != null)
            StopCoroutine(StartSequence);

        StartSequence = StartCoroutine(StartSequenceCR());
        Anim.SetBool("Started", true);               
    }

    protected override void OnStateExit()
    {
        Anim.SetBool("Started", false);
        playmodeUI.SetActive(false);
        playmodeCamera.m_Priority = 0;
    }

    public IEnumerator StartSequenceCR()
    {
        yield return new WaitForSeconds(SequenceLength);
        playmodeUI.SetActive(true);
        player.enabled = true;
        player.ResetController();
        playmodeCamera.m_Priority = 1;

        StartSequence = null;
    }
}
