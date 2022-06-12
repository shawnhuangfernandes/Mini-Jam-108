// Author:  Joseph Crump
// Date:    06/10/22

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State that belongs to the GameManager state machine.
/// </summary>
public abstract class GameState : MonoBehaviour
{
    /// <summary>
    /// Event raised whenever the game state is entered.
    /// </summary>
    public event Action Entered;

    /// <summary>
    /// Event raised whenever the game stat is exited.
    /// </summary>
    public event Action Exited;

    /// <summary>
    /// Returns the number of seconds that this state has been active.
    /// </summary>
    public float TimeInState => Time.time - timeStateEntered;

    private List<Transition> transitions = new List<Transition>();

    private bool isInitialized = false;
    private float timeStateEntered;

    private void OnEnable()
    {
        if (isInitialized == false)
        {
            OnInitialize();
            isInitialized = true;
        }

        timeStateEntered = Time.time;

        OnStateEnter();

        Entered?.Invoke();
    }

    private void Update()
    {
        OnStateUpdate();
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            return;
#endif
        OnStateExit();

        Exited?.Invoke();
    }

    /// <summary>
    /// Add a transition to the gamestate.
    /// </summary>
    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }

    /// <summary>
    /// Remove a transition from the gamestate.
    /// </summary>
    public void RemoveTransition(Transition transition)
    {
        transitions.Remove(transition);
    }

    /// <summary>
    /// Remove all transitions from the gamestate.
    /// </summary>
    public void ClearTransitions()
    {
        transitions.Clear();
    }

    /// <summary>
    /// Check all transition conditions.
    /// </summary>
    /// <param name="targetState">
    /// The target state of the first transition that returns true.
    /// </param>
    /// <returns>
    /// Returns true if any transition evaluates to true.
    /// </returns>
    public bool TryTransitions(out GameState targetState)
    {
        targetState = null;
        foreach (var transition in transitions)
        {
            if (!transition.Condition())
                continue;

            // when the condition is true
            targetState = transition.TargetState;
            return true;
        }

        return false;
    }

    protected virtual void OnInitialize() { }
    protected virtual void OnStateEnter() { }
    protected virtual void OnStateUpdate() { }
    protected virtual void OnStateExit() { }
}
