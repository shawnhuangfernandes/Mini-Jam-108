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
    private StatType affectedStat;

    [SerializeField]
    [Tooltip("Modifier applied to the stat when the upgrade is purchased.")]
    private StatModifier modifier;

    /// <summary>
    /// Apply the upgrade's modifier to the appropriate stat.
    /// </summary>
    public void Apply()
    {
        var stat = StatDirectory.Get(affectedStat);
        stat.AddModifier(modifier);
    }
}
