// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    [SerializeField]
    [Tooltip("Base purchase cost of the upgrade")]
    private int baseCost = 25;

    [SerializeField]
    private Stat affectedStat;

    [SerializeField]
    [Tooltip("Modifier applied to the stat when the upgrade is purchased.")]
    private StatModifier modifier;
}
