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
    [Tooltip("The controller for the Rock Player")]
    [SerializeField] private RockController Player;

    [Tooltip("The input for the player")]
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

    public enum Phase { Menu, Gameplay, Over }
    private Phase CurrentPhase = Phase.Menu;
    private int Points = 0;

    private Vector3 PlayerPos => Player.transform.position;


    #region Monobehavior Callbacks

    private void Awake()
    {
        Player.enabled = false;
    }
    public void Start()
    {
        SetPhase(Phase.Menu);
        StartPhase();
    }
    #endregion

    public void SetPhase(Phase _phase)
    {
        CurrentPhase = _phase;
    }

    public void StartPhase()
    {
        switch (CurrentPhase)
        {
            // === MENU START ===
            case Phase.Menu:
                Input.Skip.Pressed += LeaveMenu;

                StartUI.SetActive(true);

                if (Points > 0)
                {
                    PointsTMP.text = $"{Points} Left";
                }
                else
                {
                    PointsTMP.text = "Play To Upgrade";
                }
       
                StartCam.m_Priority = 1;
                break;

            // === PLAY START ===
            case Phase.Gameplay:
                PlayUI.SetActive(true);
                Player.enabled = true;
                Player.ResetController();
                Player.transform.position = new Vector3(PlayerPos.x, Player.StartHeight, PlayerPos.z);
                PlayCam.m_Priority = 1;
                break;

            // === GAME OVER START ===
            case Phase.Over:
                Input.Skip.Pressed += EnterMenu;

                EndUI.SetActive(true);

                EndCam.m_Priority = 1;
                break;
        }      
    }

    public void EndPhase()
    {
        switch (CurrentPhase)
        {
            // === MENU END ===
            case Phase.Menu:
                Input.Skip.Pressed -= LeaveMenu;
                StartUI.SetActive(false);               
                StartCam.m_Priority = 0;
                break;

            // === PLAY END ===
            case Phase.Gameplay:
                PlayUI.SetActive(false);
                PlayCam.m_Priority = 0;
                break;

            // === GAME OVER END ===
            case Phase.Over:
                Input.Skip.Pressed -= EnterMenu;
                EndUI.SetActive(false);
                break;
        }
    }

    public void EnterMenu()
    {
        EndPhase();
        SetPhase(Phase.Menu);
        StartPhase();
    }

    public void LeaveMenu()
    {
        EndPhase();
        SetPhase(Phase.Gameplay);
        StartPhase();
    }

    public void LoseGame()
    {
        EndPhase();
        SetPhase(Phase.Over);
        StartPhase();
    }
}
