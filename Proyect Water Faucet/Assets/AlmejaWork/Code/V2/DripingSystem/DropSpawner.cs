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
    
    [Header("Phase Settings")]
    [SerializeField, Range(1,4)] private int phase = 1;
    private int _dropsPerInterval;
    private float _distanceBetweenDrops;
    private float _intervalDuration;
    
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

    #region drop LifeCircle

    public void SpawnDrop()
    {
        if (dropPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("DropPrefab or SpawnPoint is missing");
            return;
        }
        for (int i = 0; i < _dropsPerInterval; i++)
        {
            Vector3 spawnPosition = spawnPoint.position + Vector3.right * (i * _distanceBetweenDrops);
            GameObject drop = LeanPool.Spawn(dropPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void UpdatePhase(int newPhase)
    {
        phase = Mathf.Clamp(newPhase, 1, 4);
        UpdatePhaseSettings();
    }
    
    private void UpdatePhaseSettings()
    {
        switch (phase)
        {
            case 1:
                _dropsPerInterval = 4;
                _intervalDuration = 0.25f; // 4 gotas por segundo
                _distanceBetweenDrops = 1f; // Distancia base
                break;
            case 2:
                _dropsPerInterval = 8;
                _intervalDuration = 0.125f; // 8 gotas por segundo
                _distanceBetweenDrops = 0.5f; // Distancia proporcional
                break;
            case 3:
                _dropsPerInterval = 12;
                _intervalDuration = 0.083f; // 12 gotas por segundo
                _distanceBetweenDrops = 0.33f; // Distancia proporcional
                break;
            case 4:
                _dropsPerInterval = 16;
                _intervalDuration = 0.0625f; // 16 gotas por segundo
                _distanceBetweenDrops = 0.25f; // Distancia proporcional
                break;
        }

        if (_timer != null)
        {
            _timer.SetDuration(_intervalDuration);
        }
    }

    public void DestroyDrop(GameObject drop)
    {
        LeanPool.Despawn(drop);
    }

    #endregion
    
}
