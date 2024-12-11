using System;
using UnityEngine;

public class DropCollision : MonoBehaviour
{
    #region Variables

    private DropSpawner _dropSpawner;

    #endregion

    private void Awake()
    {
        #region Finders
        _dropSpawner = GameObject.FindObjectOfType<DropSpawner>();
        if (_dropSpawner == null)
        {
            Debug.LogError("DropSpawner not Found in Scene");
        }
        

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("colisione con " + other.transform.name);
            _dropSpawner.DestroyDrop(transform.gameObject); 
        }
    }
    
}
