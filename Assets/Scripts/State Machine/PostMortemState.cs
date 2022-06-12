// Author:  Joseph Crump
// Date:    06/12/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// The state that is directed to after the final round of gameplay.
/// </summary>
public class PostMortemState : GameState
{
    [SerializeField]
    private CanvasGroup postMortemCanvasGroup;

    [SerializeField]
    [Tooltip("How long it takes for canvas group to fade in.")]
    private float canvasGroupFadeLength = 0.3f;

    protected override void OnStateEnter()
    {
        postMortemCanvasGroup.gameObject.SetActive(true);
        postMortemCanvasGroup.DOFade(1f, canvasGroupFadeLength);
    }
}
