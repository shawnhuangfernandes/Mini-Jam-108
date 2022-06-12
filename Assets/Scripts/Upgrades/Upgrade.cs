// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable Upgrade for one of the player's stats.
/// </summary>
[System.Serializable]
public class Upgrade
{
    [Header("Properties")]
    [SerializeField]
    [Tooltip("Base purchase cost of the upgrade")]
    private int baseCost = 25;

    [SerializeField]
    private StatType affectedStat;

    [SerializeField]
    [Tooltip("Modifier applied to the stat when the upgrade is purchased.")]
    private StatModifier modifier;

    /// <summary>
    /// Total cost of the upgrade when multiplied by the global cost modifier.
    /// </summary>
    public int Cost => Mathf.FloorToInt(baseCost * StatDirectory.Get(StatType.UpgradeCostMultiplier));

    /// <summary>
    /// Apply the upgrade's modifier to the appropriate stat.
    /// </summary>
    public void Apply()
    {
        var stat = StatDirectory.Get(affectedStat);
        stat.AddModifier(modifier);
    }
}
