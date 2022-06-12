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
    public delegate void UpgradePurchasedEvent(UpgradeButton button, ShopUpgrade upgrade);

    /// <summary>
    /// Event raised whenever the upgrade stored in this button is purchased.
    /// </summary>
    public event UpgradePurchasedEvent Purchased;

    [Header("References")]
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI costText;

    [SerializeField]
    [Tooltip("Color of the text when the player can afford the upgrade.")]
    private Color defaultTextColor = Color.white;

    [SerializeField]
    [Tooltip("Color of the text when the player cannot afford the upgrade.")]
    private Color unaffordableTextColor = Color.red;

    [SerializeField]
    private Button button;

    private bool isFrozen = false;
    private ShopUpgrade storedUpgrade;

    private PlayerProperty points => PlayerProperty.Points;
    private bool playerCanAffordUpgrade
    {
        get
        {
            if (storedUpgrade == null)
                return false;

            return (points >= storedUpgrade.Cost);
        }
    }

    private void OnValidate()
    {
        button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Purchase);
        points.Changed += OnPointsValueChanged;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Purchase);
        points.Changed -= OnPointsValueChanged;
    }

    /// <summary>
    /// Set the upgrade registered by this button.
    /// </summary>
    public void SetUpgrade(ShopUpgrade upgrade)
    {
        if (isFrozen)
            return;

        storedUpgrade = upgrade;
        costText.text = $"{upgrade.Cost}";
        icon.sprite = upgrade.Icon;
        background.color = upgrade.Rarity.Color;

        CheckEnabledState();
    }

    private void Purchase()
    {
        if (storedUpgrade == null)
            return;

        points.Subtract(storedUpgrade.Cost);

        storedUpgrade.Apply();

        isFrozen = false;
        Purchased?.Invoke(this, storedUpgrade);
    }

    private void CheckEnabledState()
    {
        button.interactable = playerCanAffordUpgrade;
        costText.color = playerCanAffordUpgrade 
            ? defaultTextColor 
            : unaffordableTextColor;
    }

    private void OnPointsValueChanged(float value)
    {
        CheckEnabledState();
    }
}
