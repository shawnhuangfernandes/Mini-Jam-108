// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flags used to categorize modifiers.
/// </summary>
[System.Flags]
public enum ModifierFlags
{
    None = 0,
    Upgrade = 1,
    Temporary = 2,
}

/// <summary>
/// Enumerator that determines how a modifier affects a Stat.
/// </summary>
public enum ModifierType
{
    Add,
    Subtract,
    Multiply,
    Divide,
}

/// <summary>
/// Modifier that can be applied to a <see cref="Stat"/>.
/// </summary>
[System.Serializable]
public class StatModifier
{
    [Tooltip("The value of the modifier. The value behaves defferently depending on the ModifierType.")]
    public float Value = 0f;

    [Tooltip("How the modifier affects its target stat.")]
    public ModifierType Type = ModifierType.Add;

    [Tooltip("Application order of the modifier. Lower values are applied first.")]
    public int Order = 0;

    [Tooltip("Flags belonging to this modifier.")]
    public ModifierFlags Flags;

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
        switch (Type)
        {
            case ModifierType.Add: 
                return Value;

            case ModifierType.Subtract:
                return -Value;

            case ModifierType.Multiply:
                float multipliedStatValue = statValue * this.Value;
                return multipliedStatValue - statValue;

            case ModifierType.Divide:
                float dividedStatValue = statValue / this.Value;
                return dividedStatValue - statValue;

            default:
                break;
        }

        return default;
    }
}
