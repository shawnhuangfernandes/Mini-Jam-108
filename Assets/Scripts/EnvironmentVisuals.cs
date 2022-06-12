// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that handles visuals of rock skipping
/// This includes the water movement, spawning VFX
/// </summary>
public class EnvironmentVisuals : MonoBehaviour
{
    [Header("Water Materials")]
    [SerializeField] private TextureShifter WaterShifter;
    [SerializeField] private TextureShifter SandShifter;

    [Header("Splash Effects")]
    [SerializeField] private float SplashHeight;

    private RockController _player;
    private RockController player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<RockController>();

            return _player;
        }
    }

    public void SpawnSplash(GameObject _splashPrefab)
    {
        Transform rockXFRM = player.transform;
        Vector3 spawnPosition = new Vector3(rockXFRM.position.x, SplashHeight, rockXFRM.position.z);
        ParticleSystem splash = Instantiate(_splashPrefab, spawnPosition, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    public void SetScrollSpeed(Stat _speed)
    {
        WaterShifter.SetSpeed(_speed);
        SandShifter.SetSpeed(_speed);
    }

    public void SetScrollSpeed(float _speed)
    {
        SetScrollSpeed(new Stat(_speed));
    }
}
