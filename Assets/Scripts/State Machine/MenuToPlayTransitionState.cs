// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameState for transitioning between the menu state and play state.
/// </summary>
public class MenuToPlayTransitionState : GameState
{
    /// <summary>
    /// Returns true when the serquence coroutine is complete.
    /// </summary>
    public bool SequenceFinished { get; private set; } = false;

    [SerializeField]
    private float sequenceLength = 3f;

    [SerializeField]
    [Tooltip("The animator used to drive the transition into the play sequence")]
    private Animator animator;

    private Coroutine sequenceCoroutine;

    protected override void OnStateEnter()
    {
        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(StartSequenceCR());
        animator.SetBool("Started", true);
    }

    protected override void OnStateExit()
    {
        animator.SetBool("Started", false);

        SequenceFinished = false;
    }

    public IEnumerator StartSequenceCR()
    {
        yield return new WaitForSeconds(sequenceLength);

        SequenceFinished = true;

        sequenceCoroutine = null;
    }
}
