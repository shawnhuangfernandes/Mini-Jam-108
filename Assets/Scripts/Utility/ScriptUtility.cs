// Author:  Joseph Crump
// Date:    06/10/22

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of some useful static utility methods.
/// </summary>
public static class ScriptUtility
{
    /// <summary>
    /// Perform an action at the end of the current frame.
    /// </summary>
    public static void DoAtEndOfFrame(Action action)
    {
        CoroutineRunner.RunCoroutine(DoAtEndOfFrameCoroutine(action));
    }

    private static IEnumerator DoAtEndOfFrameCoroutine(Action action)
    {
        yield return new WaitForEndOfFrame();

        action();
    }
}
