// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serialized object containing details pertaining to a rarity type.
/// </summary>
[CreateAssetMenu(fileName = "New Rarity", menuName = "Shop/Rarity")]
public class Rarity : ScriptableObject
{
    [Tooltip("Sampling weight of the rarity in the shop.")]
    public Stat ShopWeight = new Stat(100f);

    [Tooltip("Color of the rarity when displayed on an upgrade button background.")]
    public Color Color = Color.white;
}
