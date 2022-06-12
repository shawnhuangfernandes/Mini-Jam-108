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
    [Tooltip("How likely the upgrade is to appear.")]
    private RarityType rarity = RarityType.Common;

    /// <summary>
    /// Total cost of the upgrade when multiplied by the global cost modifier.
    /// </summary>
    public int Cost => Mathf.FloorToInt(baseCost * StatDirectory.Get(StatType.UpgradeCostMultiplier));

    private List<Upgrade> upgrades = new List<Upgrade>();

    /// <summary>
    /// Apply all serialized upgrades to their appropriate stats.
    /// </summary>
    public void Apply()
    {
        upgrades.ForEach(upgrade => upgrade.Apply());
    }
}
