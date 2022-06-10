// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modifier that can be applied to a <see cref="Stat"/>.
/// </summary>
[System.Serializable]
public class StatModifier
{
    public enum ModifierType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
    }

    [SerializeField]
    [Tooltip("How the modifier affects its target stat.")]
    private ModifierType type = ModifierType.Add;

    [SerializeField]
    [Tooltip("The value of the modifier. The value behaves defferently depending on the ModifierType.")]
    private float value = 0f;

    [Tooltip("Application order of the modifier. Lower values are applied first.")]
    public int Order = 0;

    /// <summary>
    /// Compute the modifier value based on the value of a stat.
    /// </summary>
    /// <param name="statValue">
    /// The value of the stat at the time that the modifier is applied.
    /// </param>
    /// <returns>
    /// Returns the modifier value only, without the stat value included.
    /// </returns>
    public float CalculateModifierValue(float statValue)
    {
        switch (type)
        {
            case ModifierType.Add: 
                return value;

            case ModifierType.Subtract:
                return -value;

            case ModifierType.Multiply:
                float multipliedStatValue = statValue * this.value;
                return multipliedStatValue - statValue;

            case ModifierType.Divide:
                float dividedStatValue = statValue / this.value;
                return dividedStatValue - statValue;

            default:
                break;
        }

        return default;
    }
}
