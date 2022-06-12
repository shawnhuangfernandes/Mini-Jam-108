// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior that manages the purchasing of <see cref="Upgrade"/>s.
/// </summary>
public class Shop : MonoBehaviour
{
    [Tooltip("Multiplier applied to all upgrades purchased in the shop.")]
    public Stat UpgradeCostMultiplier = new Stat(1f);

    [SerializeField]
    private List<ShopUpgrade> upgrades = new List<ShopUpgrade>();

    private void Start()
    {
        DeleteNullReferences();
    }

    /// <summary>
    /// Add an upgrade to the upgrades list.
    /// </summary>
    public void AddUpgrade(ShopUpgrade upgrade)
    {
        if (upgrades.Contains(upgrade))
            return;

        upgrades.Add(upgrade);
    }

    private void DeleteNullReferences()
    {
        upgrades.RemoveAll(upgrade => upgrade == null);
    }
}
