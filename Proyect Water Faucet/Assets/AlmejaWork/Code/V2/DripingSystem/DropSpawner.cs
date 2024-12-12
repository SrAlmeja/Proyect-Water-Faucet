using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Unity.Mathematics;

public class DropSpawner : MonoBehaviour
{
    #region Variables

    [Header("Dripping Settings")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform spawnPoint;

    private Timer _timer;
    private float _dropPerSecond;
    private float _frequency;
    private float _distance = 1f;
    
    #endregion

    #region Unity Methods

    private void Awake()
    {
        _timer = GameObject.FindObjectOfType<Timer>();
        if (_timer == null)
        {
            Debug.LogError("timer not Found in Scene");
        }
        else
        {
            UpdateFrequency();
            _timer.OnTimerComplete += SpawnDrop;
        }
    }

    private void OnDestroy()
    {
        if (_timer != null)
        {
            _timer.OnTimerComplete -= SpawnDrop;
        }
    }

    #endregion

    #region DropSpawnLogic

    public void SpawnDrop()
    {
        if (dropPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("DropPrefab or SpawnPoint is missing");
            return;
        }
        Vector3 spawnPosition = spawnPoint.position;
        LeanPool.Spawn(dropPrefab, spawnPosition, Quaternion.identity);
    }
    public void DestroyDrop(GameObject drop)
    {
        LeanPool.Despawn(drop);
    }

    #endregion
    
    #region DropManagement
    public void SetDropPerSecond(float newDropPerSecond)
    { 
        _dropPerSecond = Mathf.Max(0.1f, newDropPerSecond); // Evita valores negativos o cero
        UpdateFrequency();
    }

    private void UpdateFrequency()
    {
        if (_dropPerSecond > 0)
        { _frequency = _distance / _dropPerSecond;
            if (_timer != null)
            {
                _timer.SetDuration(_frequency);
            }
            
        }
    } 
    #endregion
}

