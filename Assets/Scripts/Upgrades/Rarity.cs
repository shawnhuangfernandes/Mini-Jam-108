// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Serialized object containing details pertaining to a rarity type.
/// </summary>
[CreateAssetMenu(fileName = "New Rarity", menuName = "Shop/Rarity")]
public class Rarity : ScriptableObject
{
    private static Rarity[] _allRarities;

    /// <summary>
    /// Enumerable set of all rarities.
    /// </summary>
    public static IEnumerable<Rarity> AllRarities
    {
        get
        {
            if (_allRarities == null)
                _allRarities = FindObjectsOfType<Rarity>();

            return _allRarities;
        }
    }

    [Tooltip("Sampling weight of the rarity in the shop.")]
    public Stat ShopWeight = new Stat(100f);

    [Tooltip("Color of the rarity when displayed on an upgrade button background.")]
    public Color Color = Color.white;

    /// <summary>
    /// Draw a random rarity from all rarities based on the sampling weights
    /// of each one.
    /// </summary>
    public static Rarity GetRandom()
    {
        return GetRandom(AllRarities);
    }

    /// <summary>
    /// Draw a random rarity based on the sampling weights of each rarity in
    /// a set.
    /// </summary>
    public static Rarity GetRandom(IEnumerable<Rarity> rarities)
    {
        float total = 0f;

        foreach (var rarity in rarities)
        {
            total += rarity.ShopWeight;
        }

        float roll = Random.Range(0f, total);

        foreach (var rarity in rarities)
        {
            roll -= rarity.ShopWeight;

            if (roll <= 0f)
                return rarity;
        }

        // default
        return rarities.Last();
    }
}
