// Author:  Shawn Huang Fernandes
// Date:    06/10/22

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private float ScrollSpeedModifier;

    [Header("Splash Effects")]
    [SerializeField] private float SplashHeight;

    [Header("Text Effects")]
    [SerializeField] private float TextHeight;

    [Header("Floating Objects")]
    [SerializeField] List<ObjectPool> FloatingObjects = new List<ObjectPool>();
    [SerializeField] float MinZDistance = 15F;
    [SerializeField] float MaxZDistance = 35F;
    [SerializeField] float MinXDistance = -25F;
    [SerializeField] float MaxXDistance = 25F;

    [SerializeField] float MinSpawnDuration = 2F;
    [SerializeField] float MaxSpawnDuration = 4F;
    

    private float Timer;
    private float CurrentSpawnTime;

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

    private GameManager _gm;
    private GameManager gm
    {
        get
        {
            if (_gm == null)
                _gm = FindObjectOfType<GameManager>();

            return _gm;
        }
    }

    private void Update()
    {
        if (gm.CurrentState == gm.PlayState)
        {
            Timer += Time.deltaTime;

            if (Timer > CurrentSpawnTime)
            {
                SpawnFloater();
                Timer = 0F;
            }
        }
    }

    public void SpawnSplash(GameObject _splashPrefab)
    {
        Transform rockXFRM = player.transform;
        Vector3 spawnPosition = new Vector3(rockXFRM.position.x, SplashHeight, rockXFRM.position.z);
        ParticleSystem splash = Instantiate(_splashPrefab, spawnPosition, Quaternion.identity).GetComponent<ParticleSystem>();
    }

    public void SpawnText(GameObject _textPrefab)
    {
        Transform rockXFRM = player.transform;
        GameObject textGO = Instantiate(_textPrefab, rockXFRM);
        textGO.transform.localPosition = new Vector3(0F, TextHeight, 0F);
    }

    public void SpawnFloater()
    {
        float xPos = UnityEngine.Random.Range(MinXDistance, MaxXDistance);
        float zPos = UnityEngine.Random.Range(MinZDistance, MaxZDistance);
        Vector3 playerPos = player.transform.position;

        Vector3 spawnPosition = new Vector3(playerPos.x + xPos, 0F, playerPos.z + zPos);

        
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

[Serializable]
public class ObjectPool
{
    [Header("Pool Data")]
    [SerializeField] private string PoolName;
    [SerializeField] private float DistanceThreshold;
    [SerializeField] private Transform PoolContainer;
    [SerializeField] private PooledObject Prefab;
    [SerializeField] private float SpawnHeight;
    [SerializeField] private int MaxCount = 10;

    private List<PooledObject> Unavailable = new List<PooledObject>();
    private List<PooledObject> Available = new List<PooledObject>();

    public void SpawnObject(Vector3 _position)
    {
        if (Available.Count > 0)
        {
            PooledObject selected = Available.First();
            Available.Remove(selected);

            Unavailable.Add(selected);
            selected.transform.position = _position;
            selected.gameObject.SetActive(true);
        }
        else
        {
            if (Unavailable.Count >= MaxCount)
            {
                Debug.LogWarning($"Reached Max Limit for {Prefab} pool.");
                return;
            }
            else if (Unavailable.Count < MaxCount)
            {
                PooledObject selected = GameObject.Instantiate(Prefab.gameObject, PoolContainer.transform).GetComponent<PooledObject>();
                selected.Pool = this;
                Unavailable.Add(selected);
                selected.transform.position = _position;
            }
        }
    }

    public void SetAvailable(PooledObject _pooledObject)
    {
        Available.Add(_pooledObject);
        Unavailable.Remove(_pooledObject);
    }
}
