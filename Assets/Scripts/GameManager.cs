// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game phase management
/// </summary>

public class GameManager : MonoBehaviour
{

    [Header("Game Start")]
    [SerializeField]
    [Tooltip("The UI display on game start")]
    private GameObject StartUI;

    [Header("Game Play")]
    [SerializeField]
    [Tooltip("The UI display on during rock skipping")]
    public GameObject PlayUI;

    [Header("Game End")]
    [SerializeField]
    [Tooltip("The UI display on game end")]
    private GameObject EndUI;

    public enum Phase {  GameStart, GamePlay, GameEnd }
    private Phase CurrentPhase;

    #region Monobehavior Callbacks
    public void Start()
    {
        SetPhase(Phase.GameStart);
        StartPhase();
    }
    #endregion

    public void SetPhase(Phase _phase)
    {
        CurrentPhase = _phase;
    }

    public void StartPhase()
    {
        switch(CurrentPhase)
        {
            case Phase.GameStart:
                StartUI.SetActive(true);
                break;
            case Phase.GamePlay:
                PlayUI.SetActive(true);
                break;
            case Phase.GameEnd:
                EndUI.SetActive(true);
                break;
        }      
    }

    public void EndPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.GameStart:
                StartUI.SetActive(false);
                break;
            case Phase.GamePlay:
                PlayUI.SetActive(false);
                break;
            case Phase.GameEnd:
                EndUI.SetActive(false);
                break;
        }
    }

    public void TrackPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.GameStart:

                break;
            case Phase.GamePlay:

                break;
            case Phase.GameEnd:

                break;
        }
    }
}


