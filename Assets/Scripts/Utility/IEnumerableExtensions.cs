// Author:  Joseph Crump
// Date:    06/11/22

using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extension methods for <see cref="IEnumerable"/> objects.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Get a random element from an enumerable collection.
    /// </summary>
    public static T Random<T>(this IEnumerable<T> enumerable)
    {
        int index = UnityEngine.Random.Range(0, enumerable.Count());

        return enumerable.ElementAt(index);
    }
}
