// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Behavior that drives an upgrade button for the shop system.
/// </summary>
[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    private Button button;

    private void OnValidate()
    {
        button ??= GetComponent<Button>();
    }

    /// <summary>
    /// Set the upgrade registered by this button.
    /// </summary>
    public void SetUpgrade(ShopUpgrade upgrade)
    {
        costText.text = $"{upgrade.Cost}";
        icon.sprite = upgrade.Icon;
        background.color = upgrade.Rarity.Color;
    }
}
