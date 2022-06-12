// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class used to route <see cref="StatType"/> references to the 
/// proper stat instance in the game.
/// </summary>
public static class StatDirectory
{
    private static RockController _player;
    private static RockController player
    {
        get
        {
            if (_player == null)
                _player = Object.FindObjectOfType<RockController>();

            return _player;
        }
    }

    private static Shop _shop;
    private static Shop shop
    {
        get
        {
            if (_shop == null)
                _shop = Object.FindObjectOfType<Shop>();

            return _shop;
        }
    }

    /// <summary>
    /// Get a stat by its <see cref="StatType"/> identifier.
    /// </summary>
    public static Stat Get(StatType statType)
    {
        switch (statType)
        {
            case StatType.ThresholdSize:
                return player.ThresholdSize;

            case StatType.StartHeight:
                return player.StartHeight;

            case StatType.BounceDegradationMultiplier:
                return player.BounceDegradationMultiplier;

            case StatType.FaultyDegradationMultiplier:
                return player.FaultyDegradationMultiplier;

            case StatType.Gravity:
                return player.Gravity;

            case StatType.PrematureSkipCooldown:
                return player.PrematureSkipCooldown;

            case StatType.LateralSpeed:
                return player.LateralSpeed;

            case StatType.UpgradeCostMultiplier:
                return shop.UpgradeCostMultiplier;

            default:
                break;
        }

        return null;
    }
}
