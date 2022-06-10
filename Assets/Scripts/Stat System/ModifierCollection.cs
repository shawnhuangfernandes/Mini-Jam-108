// Author:  Joseph Crump
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// List of modifiers that sorts inserted values according to the modifiers'
/// sort orders.
/// </summary>
/// <remarks>
/// This way iterating over the modifiers applies their effects
/// in the correct order without needing to resort for every calculation.
/// </remarks>
public class ModifierCollection : IList<StatModifier>
{
    private List<StatModifier> modifiers = new List<StatModifier>();

    public StatModifier this[int index]
    { 
        get => modifiers[index];
        set => modifiers[index] = value;
    }

    public int Count => modifiers.Count;

    public bool IsReadOnly => ((ICollection<StatModifier>)modifiers).IsReadOnly;

    public void Add(StatModifier item)
    {
        StatModifier nextInOrder = null;

        try
        {
            nextInOrder = modifiers.First(m => m.Order > item.Order);
        }
        catch (System.InvalidOperationException) // no element found
        {
            nextInOrder = null;
        }

        if (nextInOrder != null)
        {
            int index = modifiers.IndexOf(nextInOrder);
            modifiers.Insert(index, item);
            return;
        }

        modifiers.Add(item);
    }

    public void Clear()
    {
        modifiers.Clear();
    }

    public bool Contains(StatModifier item)
    {
        return modifiers.Contains(item);
    }

    public void CopyTo(StatModifier[] array, int arrayIndex)
    {
        modifiers.CopyTo(array, arrayIndex);
    }

    public IEnumerator<StatModifier> GetEnumerator()
    {
        return ((IEnumerable<StatModifier>)modifiers).GetEnumerator();
    }

    public int IndexOf(StatModifier item)
    {
        return modifiers.IndexOf(item);
    }

    public void Insert(int index, StatModifier item)
    {
        modifiers.Insert(index, item);
    }

    public bool Remove(StatModifier item)
    {
        return modifiers.Remove(item);
    }

    public void RemoveAt(int index)
    {
        modifiers.RemoveAt(index);
    }

    /// <summary>
    /// Remove any modifiers that have the given flags.
    /// </summary>
    public void RemoveByFlag(ModifierFlags flags)
    {
        modifiers.RemoveAll(modifier => modifier.Flags.HasFlag(flags));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)modifiers).GetEnumerator();
    }
}
