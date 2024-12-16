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
    [SerializeField] private GameObject spawnPoint;

    private Timer _timer;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _timer = FindFirstObjectByType<Timer>();
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

    #region DropSpawnLogic

    public void SpawnDrop()
    {
        if (dropPrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("DropPrefab or SpawnPoint is missing");
            return;
        }
        Vector3 spawnPosition = spawnPoint.transform.position;

        LeanPool.Spawn(dropPrefab, spawnPosition, Quaternion.identity, spawnPoint.transform.parent);
    }
    public void DestroyDrop(GameObject drop)
    {
        LeanPool.Despawn(drop);
    }

    #endregion
}

