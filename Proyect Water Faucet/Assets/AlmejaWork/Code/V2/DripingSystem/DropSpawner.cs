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
    [SerializeField] private Transform spawnPont;

    private Timer _timer;
    
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
        if (dropPrefab != null && spawnPont != null)
        {
            GameObject drop = LeanPool.Spawn(dropPrefab, spawnPont.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("DropPrefab or SpawnPoint is missing");
        }
    }

    public void DestroyDrop(GameObject drop)
    {
        LeanPool.Despawn(drop);
    }

    #endregion
    
}
