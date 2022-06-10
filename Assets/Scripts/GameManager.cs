// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Game phase management
/// </summary>

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("The input for the player")]
    [SerializeField] private GameObject Player;
    [SerializeField] private InputHandler Input;

    [Header("Game Start")]
    [Tooltip("The UI display on game start")]
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject UpgradesContainer;
    [SerializeField] private TextMeshProUGUI PointsTMP;
    [Tooltip("The camera view for the start menu")]
    [SerializeField] private CinemachineVirtualCamera StartCam;

    [Header("Game Play")]
    [Tooltip("The UI display on during rock skipping")]
    [SerializeField] private GameObject PlayUI;
    [Tooltip("The player follow camera view for gameplay")]
    [SerializeField] private CinemachineVirtualCamera PlayCam;

    [Header("Game End")]
    [Tooltip("The UI display on game end")]
    [SerializeField] private GameObject EndUI;
    [Tooltip("The camera for the game end")]
    [SerializeField] private CinemachineVirtualCamera EndCam;

    public enum Phase {  Menu, GamePlay, Over }
    private Phase CurrentPhase = Phase.Menu;

    private int Points = 0;

    #region Monobehavior Callbacks

    public void Start()
    {
        SetPhase(Phase.Menu);
        StartPhase();
    }

    public void Update()
    {
        switch(CurrentPhase)
        {
            case Phase.Menu:                               
                break;
            case Phase.GamePlay:
                PlayUI.SetActive(true);
                break;
            case Phase.Over:
                EndUI.SetActive(true);
                break;
        }
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
            // === MENU START ===
            case Phase.Menu:
                StartUI.SetActive(true);
                if (Points > 0)
                {
                    PointsTMP.text = $"{Points} Left";

                }
                else
                {
                    PointsTMP.text = "Play To Upgrade";

                }

                Input.Skip.Pressed += LeaveMenu;
                StartCam.m_Priority = 1;
                break;

            // === PLAY START ===
            case Phase.GamePlay:
                PlayUI.SetActive(true);
                Player.SetActive(true);
                PlayCam.m_Priority = 1;
                break;

            // === GAME OVER START ===
            case Phase.Over:
                EndUI.SetActive(true);
                EndCam.m_Priority = 1;
                break;
        }      
    }

    public void EndPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.Menu:
                Input.Skip.Pressed -= LeaveMenu;
                StartUI.SetActive(false);               
                StartCam.m_Priority = 0;
                break;
            case Phase.GamePlay:
                PlayUI.SetActive(false);
                PlayCam.m_Priority = 0;
                break;
            case Phase.Over:
                EndUI.SetActive(false);
                break;
        }
    }

    public void TrackPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.Menu:

                break;
            case Phase.GamePlay:

                break;
            case Phase.Over:

                break;
        }
    }

    public void LeaveMenu()
    {
        EndPhase();
        SetPhase(Phase.GamePlay);
        StartPhase();
    }
}


