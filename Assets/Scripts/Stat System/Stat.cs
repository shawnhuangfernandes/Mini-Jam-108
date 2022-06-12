// Author:  Joseph Crump
// Date:    06/10/22

using UnityEngine;

/// <summary>
/// Floating-point wrapper that can have modifiers added to it.
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField]
    private float _baseValue = 0f;

    public float BaseValue => _baseValue;
    public float ModifierValue { get; private set; } = 0f;
    public float Value => BaseValue + ModifierValue;

    private ModifierCollection modifiers = new ModifierCollection();

    public Stat()
    {
        _baseValue = 0f;
    }

    public Stat(float baseValue)
    {
        _baseValue = baseValue;
    }

    /// <summary>
    /// Add a modifier to the stat.
    /// </summary>
    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        RecalculateModifiers();
    }

    /// <summary>
    /// Remove a modifier from the stat.
    /// </summary>
    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
        RecalculateModifiers();
    }

    /// <summary>
    /// Clear all stat modifiers.
    /// </summary>
    public void ClearModifiers()
    {
        modifiers.Clear();
        RecalculateModifiers();
    }

    /// <summary>
    /// Remove any modifiers that have the given flags.
    /// </summary>
    public void RemoveModifiersByFlag(ModifierFlags flags)
    {
        modifiers.RemoveByFlag(flags);
        RecalculateModifiers();
    }

    private void RecalculateModifiers()
    {
        float modValue = 0f;

        foreach (var modifier in modifiers)
        {
            float statValue = BaseValue + modValue;
            modValue += modifier.CalculateModifierValue(statValue);
        }

        ModifierValue = modValue;
    }

    public static implicit operator float(Stat stat)
    {
        return stat.Value;
    }
}
