using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables

    [Header("Testing")]
    [SerializeField] private bool ispaused;
    [SerializeField] private SOBoolean isPausedScriptable;

    [Header("Dripping Settings")] [SerializeField]
    private GameObject drop;
    [SerializeField] private float dropSpeed;
    [SerializeField] private float dropPerSecond;
    
    #endregion
    #region ScriptsToFing
    
    private DropSpawner _dropSpawner;

    #endregion

    private void Awake()
    {
        //Se encarga de verificar que mis scripts se encuentren en escena antes de cargar el resto de cosas, asi ya no es necesario colocarlo a mano
        #region Finders
        
        _dropSpawner = GameObject.FindObjectOfType<DropSpawner>();
        if (_dropSpawner == null)
        {
            Debug.LogError("DropSpawner not Found in Scene");
        }

        #endregion
    }

    private void Update()
    {
        GravitySwitch();
    }

    private void GravitySwitch()
    {
        isPausedScriptable.value = ispaused;
    }
    
}
