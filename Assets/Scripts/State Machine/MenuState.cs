// Author:  Joseph Crump
// Date:    06/10/22

using Cinemachine;
using UnityEngine;
using TMPro;
using JC.Audio2D;
using System.Collections;

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
    [Tooltip("The UI containing all the shop elements.")]
    private GameObject shopUI;

    [SerializeField]
    [Tooltip("TextMesh that displays the player's point total.")]
    private TextMeshProUGUI PointsTMP;

    [SerializeField]
    [Tooltip("One shot sound on menu start")]
    private SoundEffect StartOneShot;

    [SerializeField]
    [Tooltip("Main Menu Soundtrack")]
    private Soundtrack MenuTrack;

    [SerializeField]
    private float TrackDelay;

    private GameManager _gameManager;
    private GameManager gameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = FindObjectOfType<GameManager>();

            return _gameManager;
        }
    }

    

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
        menuCamera.m_Priority = 1;
        StartOneShot.Play();

        menuUI.SetActive(true);

        bool showShop = (gameManager.CurrentRound > 0);
        shopUI.SetActive(showShop);

        StartCoroutine(DelayedTrack());
    }

    protected override void OnStateExit()
    {
        menuUI.SetActive(false);
        menuCamera.m_Priority = 0;
        MenuTrack.Stop();
    }

    IEnumerator DelayedTrack()
    {
        yield return new WaitForSeconds(TrackDelay);
        MenuTrack.Play();
    }
}
