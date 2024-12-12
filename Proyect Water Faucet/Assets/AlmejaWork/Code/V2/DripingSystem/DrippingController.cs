using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables
/*
    [Header("Testing")] [SerializeField] private bool ispaused;*/

    [Header("Dripping Settings")]
    [SerializeField] private float dropDuration;
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
        
        lastDropFrequency = dropDuration;
    }

    private void Update()
    {
        GravitySwitch();
        SetSpeed();
        if (dropDuration != lastDropFrequency)
        {
            SetTimer();
            lastDropFrequency = dropDuration; // Actualiza la última frecuencia
        }
    }

    #endregion

    #region DropPhysics

    private void SetSpeed()
    {
        dropSpeed.value = dSpeed;
    }

    private void GravitySwitch()
    {
        isPausedScriptable.value = _uiController.IsPaused;
    }

    #endregion

    #region SpawnerFunctions

    public void UpdateDropPerSecond(float newDropPerSecond)
    {
        if (_dropSpawner != null)
        {
            _dropSpawner.SetDropPerSecond(newDropPerSecond);
        }
    }

    #endregion

    #region Timer

    // Ajusta el temporizador con la nueva duración
    private void SetTimer()
    {
        if (_timer != null)
        {
            if (dropDuration > 0)
            {
                _timer.SetInterval(dropDuration); // Usa la duración directamente sin necesidad de inverso
            }
            else
            {
                Debug.LogWarning("dropintergval es cero o negativo, el temporizador no se actualizará.");
            }
        }
    }

    #endregion
}
