// Author:  Joseph Crump
// Date:    06/12/22

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Description showing what an upgrade does or is called.
/// </summary>
public class UpgradeDescription : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How long it takes the canvas group to fade in or out.")]
    private float fadeDuration = .2f;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI upgradeName;

    [SerializeField]
    private TextMeshProUGUI upgradeDescription;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private bool hidden = true;

    /// <summary>
    /// Show the name and description for an upgrade.
    /// </summary>
    public void ShowUpgrade(ShopUpgrade upgrade)
    {
        upgradeName.text = upgrade.Name;
        upgradeDescription.text = upgrade.Description;

        Show();
    }

    /// <summary>
    /// Show the description.
    /// </summary>
    public void Show()
    {
        if (!hidden)
            return;

        hidden = false;
        canvasGroup.DOFade(1f, fadeDuration);
    }

    /// <summary>
    /// Hide the description.
    /// </summary>
    public void Hide()
    {
        if (hidden)
            return;

        hidden = true;
        canvasGroup.DOFade(0f, fadeDuration);
    }
}
