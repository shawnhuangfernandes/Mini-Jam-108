// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// GameState for transitioning between the menu state and play state.
/// </summary>
public class MenuToPlayTransitionState : GameState
{
    /// <summary>
    /// Returns true when the serquence coroutine is complete.
    /// </summary>
    public bool SequenceFinished { get; private set; } = false;

    [Header("Properties")]
    [SerializeField]
    private float sequenceLength = 3f;

    [SerializeField]
    private float roundDisplayDuration = 2f;

    [SerializeField]
    [Tooltip("How long it takes for the round display text to fade in and out.")]
    private float roundDisplayFadeLength = 0.3f;

    [Header("References")]
    [SerializeField]
    [Tooltip("The UI group showing the current round timer.")]
    private CanvasGroup roundDisplayUI;

    [SerializeField]
    [Tooltip("The animator used to drive the transition into the play sequence")]
    private Animator animator;

    private Coroutine sequenceCoroutine;

    protected override void OnStateEnter()
    {
        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(StartSequenceCR());
    }

    protected override void OnStateExit()
    {
        animator.SetBool("Started", false);

        SequenceFinished = false;
    }

    public IEnumerator StartSequenceCR()
    {
        roundDisplayUI.DOFade(1f, roundDisplayFadeLength);

        yield return new WaitForSeconds(roundDisplayDuration);

        roundDisplayUI.DOFade(0f, roundDisplayFadeLength);

        animator.SetBool("Started", true);

        yield return new WaitForSeconds(sequenceLength);

        SequenceFinished = true;

        sequenceCoroutine = null;
    }
}
