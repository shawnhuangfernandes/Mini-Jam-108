// Author:  Joseph Crump
// Date:    06/10/22

using Cinemachine;
using UnityEngine;
using TMPro;

/// <summary>
/// Gamestate for the main menu / upgrade segment.
/// </summary>
public class MenuState : GameState
{
    [SerializeField]
    [Tooltip("The camera view for the start menu")]
    private CinemachineVirtualCamera menuCamera;

    [SerializeField]
    [Tooltip("The UI display on game start.")]
    private GameObject menuUI;

    [SerializeField]
    [Tooltip("TextMesh that displays the player's point total.")]
    private TextMeshProUGUI PointsTMP;

    /// <summary>
    /// Number of points the player currently has.
    /// </summary>
    public int Points { get; private set; } = 0;

    protected override void OnInitialize()
    {
        PlayerProperty.Distance.Value = 0;
        PlayerProperty.Points.Value = 0;
    }

    protected override void OnStateEnter()
    {
        menuUI.SetActive(true);

        if (Points > 0)
        {
            PointsTMP.text = $"{Points} Left";
        }
        else
        {
            PointsTMP.text = "Play To Earn Points";
        }

        menuCamera.m_Priority = 1;
    }

    protected override void OnStateExit()
    {
        menuUI.SetActive(false);
        menuCamera.m_Priority = 0;
    }
}
