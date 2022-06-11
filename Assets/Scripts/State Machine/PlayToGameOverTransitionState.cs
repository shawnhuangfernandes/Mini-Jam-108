// Author:  Joseph Crump
// Date:    06/11/22

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameState for transitioning between the menu state and play state.
/// </summary>
public class PlayToGameOverTransitionState : GameState
{
    /// <summary>
    /// Returns true when the serquence coroutine is complete.
    /// </summary>
    public bool SequenceFinished { get; private set; } = false;

    [SerializeField]
    [Tooltip("The camera for the game end.")]
    private CinemachineVirtualCamera gameOverCamera;

    [SerializeField]
    private float sequenceLength = 3f;


    private Coroutine sequenceCoroutine;

    protected override void OnStateEnter()
    {
        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(StartSequenceCR());

        gameOverCamera.m_Priority = 1;
    }

    protected override void OnStateExit()
    {
        SequenceFinished = false;
    }

    public IEnumerator StartSequenceCR()
    {
        yield return new WaitForSeconds(sequenceLength);

        SequenceFinished = true;

        sequenceCoroutine = null;
    }
}
