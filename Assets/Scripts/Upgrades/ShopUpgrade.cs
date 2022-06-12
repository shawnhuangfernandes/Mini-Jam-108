// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asset representing an upgrade purchaseable from the shop.
/// </summary>
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Shop/Upgrade")]
public class ShopUpgrade : ScriptableObject
{
    [SerializeField]
    [Tooltip("Base purchase cost of the upgrade")]
    private int baseCost = 25;

    [SerializeField]
    [Tooltip("The rarity category of the upgrade.")]
    private Rarity rarity;

    [SerializeField]
    [Tooltip("The icon displayed by this upgrade on the Upgrade button.")]
    private Sprite icon;

    /// <summary>
    /// Total cost of the upgrade when multiplied by the global cost modifier.
    /// </summary>
    public int Cost => Mathf.FloorToInt(baseCost * StatDirectory.Get(StatType.UpgradeCostMultiplier));

    /// <summary>
    /// The icon displayed by this upgrade on the Upgrade button. 
    /// </summary>
    public Sprite Icon => icon;

    /// <summary>
    /// The rarity category of the upgrade.
    /// </summary>
    public Rarity Rarity => rarity;

    private List<Upgrade> upgrades = new List<Upgrade>();

    /// <summary>
    /// Apply all serialized upgrades to their appropriate stats.
    /// </summary>
    public void Apply()
    {
        upgrades.ForEach(upgrade => upgrade.Apply());
    }

    private void Awake()
    {
        var shop = FindObjectOfType<Shop>();
        shop.AddUpgrade(this);
    }
}
