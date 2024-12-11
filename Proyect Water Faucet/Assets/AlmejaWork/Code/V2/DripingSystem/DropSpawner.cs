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
    [SerializeField] private float dropPerSecond;
    [SerializeField] private Transform spawnPont;
    [SerializeField] private SOBoolean isPaused;
    private float dropInterval;
    private Coroutine dropFallCoroutine;
    
    #endregion

    #region Unity Methods

    private void Start()
    {
        UpdateDropInteval();
        if (!isPaused.value)
        {
            StartDropFall();
        }
    }
    
    private void Update()
    {
        ManagePausedStage();
    }

    #endregion

    #region DropManagment

    private void StartDropFall()
    {
        if (dropFallCoroutine == null)
        {
            dropFallCoroutine = StartCoroutine(DropFall());
        }
    }

    private void StopDropFall()
    {
        if (dropFallCoroutine != null)
        {
            StopCoroutine(dropFallCoroutine);
            dropFallCoroutine = null;
        }
    }

    private IEnumerator DropFall()
    {
        while (true)
        {
            SpawnDrop();
            yield return new WaitForSeconds(dropInterval);
        }
    }

    private void UpdateDropInteval()
    {
        dropInterval = 1f / Mathf.Max(0.1f, dropPerSecond);
    }
    
    #endregion

    #region PauseManage

    private void ManagePausedStage()
    {
        if (isPaused.value)
        {
            StopDropFall();
        }
        else
        {
            StartDropFall();
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
    }

    public void DestroyDrop(GameObject drop)
    {
        LeanPool.Despawn(drop);
    }

    #endregion
    
    
}
