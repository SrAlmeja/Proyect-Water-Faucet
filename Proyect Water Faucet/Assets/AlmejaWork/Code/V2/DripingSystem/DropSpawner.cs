using System;
using System.Collections;
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
    [SerializeField] private float fallDistance;
    [SerializeField] private SOBoolean isPaused;
    private float dropInterval;
    private Coroutine dripCoroutine;
    
    #endregion
    
    private void Start()
    {
        if (!isPaused.value)
        {
            dripCoroutine = StartCoroutine(Drip());
        }
    }

    private void Update()
    {
        UpdateDropInteval();
        PauseStates();
    }

    private IEnumerator Drip()
    {
        while (true)
        {
            SpawnDrop();

            yield return new WaitForSeconds(dropInterval);
        }
    }

    private void UpdateDropInteval()
    {
        dropInterval = 1f / dropPerSecond;
        
        if (dripCoroutine != null)
        {
            StopCoroutine(dripCoroutine);
            dripCoroutine = StartCoroutine(Drip());
        }
    }
    
    private void SpawnDrop()
    {
        if (dropPrefab != null && spawnPont != null)
        {
            GameObject drop = LeanPool.Spawn(dropPrefab, spawnPont.position, Quaternion.identity);
            DestroyDrop(drop);
        }
    }

    private void DestroyDrop(GameObject drop)
    {
        float fallTime = math.sqrt(2 * fallDistance / Physics.gravity.magnitude);
        
        LeanPool.Despawn(drop, fallTime);
    }

    private void PauseStates()
    {
        if (isPaused.value && dripCoroutine != null)
        {
            StopCoroutine(dripCoroutine);
            dripCoroutine = null;
        }
        else if(!isPaused.value && dripCoroutine == null)
        {
            dripCoroutine = StartCoroutine(Drip());
        }
    }
    
    public void SetDropPerSecond(float newRate)
    {
        dropPerSecond = Mathf.Max(0.1f, newRate);
        dropInterval = 1f / dropPerSecond;
    }
}
