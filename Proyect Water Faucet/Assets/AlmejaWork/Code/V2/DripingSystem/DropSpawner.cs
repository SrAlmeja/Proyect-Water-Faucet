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
    
    #endregion



    private void Start()
    {
        dropInterval = 1f / dropPerSecond;

        StartCoroutine(Drip());
    }

    private IEnumerator Drip()
    {
        while (true)
        {
            SpawnDrop();

            yield return new WaitForSeconds(dropInterval);
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
    public void SetDropPerSecond(float newRate)
    {
        dropPerSecond = Mathf.Max(0.1f, newRate);
        dropInterval = 1f / dropPerSecond;
    }
}
