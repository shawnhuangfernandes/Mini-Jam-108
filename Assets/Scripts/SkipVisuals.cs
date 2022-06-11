// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component that handles visuals of rock skipping
/// This includes the water movement, spawning VFX
/// </summary>

public class SkipVisuals : MonoBehaviour
{
    [Header("Water Materials")]
    [SerializeField] private Material WaterMaterial;
    [SerializeField] private Material SandMaterial;

    [Header("Splash Effects")]
    [SerializeField] private GameObject SplashParticle;
    [SerializeField] private float SplashHeight;

    [Header("Speed")]
    public float Speed = 0F;

    public void SpawnSplash()
    {
        Debug.Log("Doing this!");
        Transform rockXFRM = FindObjectOfType<RockController>().transform;
        Vector3 spawnPosition = new Vector3(rockXFRM.position.x, SplashHeight, rockXFRM.position.z);
        ParticleSystem splash = Instantiate(SplashParticle, spawnPosition, Quaternion.identity).GetComponent<ParticleSystem>();
    }
}
