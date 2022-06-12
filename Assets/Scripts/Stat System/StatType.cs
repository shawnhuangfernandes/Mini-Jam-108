// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ID used to identify a stat from the <see cref="StatDirectory"/>.
/// </summary>
public enum StatType
{
    ThresholdSize,
    StartHeight,
    BounceDegradationMultiplier,
    FaultyDegradationMultiplier,
    Gravity,
    PrematureSkipCooldown,
    LateralSpeed,
    UpgradeCostMultiplier,
}
