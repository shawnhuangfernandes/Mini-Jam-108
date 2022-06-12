// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Behavior that manages the purchasing of <see cref="Upgrade"/>s.
/// </summary>
public class Shop : MonoBehaviour
{
    [Tooltip("Multiplier applied to all upgrades purchased in the shop.")]
    public Stat UpgradeCostMultiplier = new Stat(1f);

    [SerializeField]
    private List<ShopUpgrade> upgrades = new List<ShopUpgrade>();

    private UpgradeButton[] upgradeButtons;

    private GameManager _gameManager;
    private GameManager gameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = FindObjectOfType<GameManager>();

            return _gameManager;
        }
    }

    private void Start()
    {
        DeleteNullReferences();

        upgradeButtons = FindObjectsOfType<UpgradeButton>(includeInactive: true);

        gameManager.MenuState.Entered += OnMenuGameStateEntered;
    }

    private void OnEnable()
    {
        UpgradeButton.Purchased += OnAnyUpgradePurchased;
    }

    private void OnDisable()
    {
        UpgradeButton.Purchased -= OnAnyUpgradePurchased;
    }

    /// <summary>
    /// Add an upgrade to the upgrades list.
    /// </summary>
    public void AddUpgrade(ShopUpgrade upgrade)
    {
        if (upgrades.Contains(upgrade))
            return;

        upgrades.Add(upgrade);
    }

    /// <summary>
    /// Get a random upgrade from the list of upgrades.
    /// </summary>
    /// <returns>
    /// Returns null if there are no upgrades in the shop.
    /// </returns>
    public ShopUpgrade GetRandomUpgrade()
    {
        if (upgrades.Count == 0)
            return null;

        var rarities = upgrades
            .Select(u => u.Rarity)
            .Distinct();

        var rarity = Rarity.GetRandom(rarities);

        return upgrades
            .Where(u => u.Rarity == rarity)
            .Random();
    }

    /// <summary>
    /// Reroll the upgrade contained by each upgrade button.
    /// </summary>
    public void RerollUpgradeButtons()
    {
        foreach (var button in upgradeButtons)
        {
            RerollUpgradeButton(button);
        }
    }

    /// <summary>
    /// Reroll the upgrade contained by an upgrade button.
    /// </summary>
    public void RerollUpgradeButton(UpgradeButton button)
    {
        button.SetUpgrade(GetRandomUpgrade());
    }

    private void DeleteNullReferences()
    {
        upgrades.RemoveAll(upgrade => upgrade == null);
    }

    private void OnMenuGameStateEntered()
    {
        RerollUpgradeButtons();
    }

    private void OnAnyUpgradePurchased(UpgradeButton button, ShopUpgrade upgrade)
    {
        RerollUpgradeButton(button);
    }
}
