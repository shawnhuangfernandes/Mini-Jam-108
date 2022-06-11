// Author:  Joseph Crump
// Date:    06/10/22

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A transition for a state in a state machine.
/// </summary>
[System.Serializable]
public class Transition
{
    /// <summary>
    /// The state that this transition leads to.
    /// </summary>
    public GameState TargetState;

    /// <summary>
    /// The function that must return true to trigger the transition.
    /// </summary>
    public Func<bool> Condition;
}
