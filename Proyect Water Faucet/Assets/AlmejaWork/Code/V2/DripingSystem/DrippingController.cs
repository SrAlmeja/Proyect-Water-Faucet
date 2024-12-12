using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables
/*
    [Header("Testing")] [SerializeField] private bool ispaused;*/

    [Header("Dripping Settings")]
    [SerializeField] private float dropPerSecond;
    [SerializeField] private float lastDropPerSecond;
    [SerializeField] private float dropFrequency;
    [SerializeField] private float dSpeed;
    [SerializeField] private SOFloat dropSpeed;
    
    [Header("Pause Settings")]
    [SerializeField] private SOBoolean isPausedScriptable;
    
    private float lastDropFrequency;

    #endregion

    #region ScriptsToFind

    private DropSpawner _dropSpawner;
    private Timer _timer;
    private UIControllerV2 _uiController;

    #endregion

    #region UnityFunctions

    private void Awake()
    {
        //Se encarga de verificar que mis scripts se encuentren en escena antes de cargar el resto de cosas, asi ya no es necesario colocarlo a mano

        #region Finders

        _dropSpawner = GameObject.FindObjectOfType<DropSpawner>();
        if (_dropSpawner == null)
        {
            Debug.LogError("DropSpawner not Found in Scene");
        }

        _timer = GameObject.FindObjectOfType<Timer>();
        if (_timer == null)
        {
            Debug.LogError("timer not Found in Scene");
        }
        
        _uiController = GameObject.FindObjectOfType<UIControllerV2>();
        if (_timer == null)
        {
            Debug.LogError("UIControllerV2 not Found in Scene");
        }

        #endregion
        
        lastDropFrequency = dropFrequency;
        lastDropPerSecond = dropPerSecond;
    }

    private void Update()
    {
        SetSpeed();
        if (dropFrequency != lastDropFrequency)
        {
            lastDropFrequency = dropFrequency; // Actualiza la última frecuencia
        }

        if (dropPerSecond != lastDropPerSecond)
        {
            UpdateDropPerSecond(dropPerSecond);
            Debug.Log("dropPerSecond updated to: " + dropPerSecond);    
        }
    }

    #endregion

    #region DropPhysics

    private void SetSpeed()
    {
        dropSpeed.value = dSpeed;
    }
    

    #endregion

    #region SpawnerFunctions

    public void UpdateDropPerSecond(float newDropPerSecond)
    {
        if (_dropSpawner != null)
        {
            _dropSpawner.SetDropPerSecond(newDropPerSecond);
            Debug.Log("Called SetDropPerSecond on DropSpawner with value: " + newDropPerSecond);
        }
    }

    #endregion

    #region Timer

    

    #endregion
}
