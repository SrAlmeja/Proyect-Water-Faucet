using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables
/*
    [Header("Testing")] [SerializeField] private bool ispaused;*/

    [Header("Dripping Settings")]
    public float DropPerSecond = 0f;
    [SerializeField] private float lastDropPerSecond;
    [SerializeField] private float dropFrequency = 0f;
    [SerializeField] private float dSpeed;
    [SerializeField] private SOFloat dropSpeed;
    
    [Header("Pause Settings")]
    [SerializeField] private SOBoolean isPausedScriptable;
    
    private float lastDropFrequency;
    
    #region ScriptsToFind

    private DropController _dropController;
    private Timer _timer;
    private UIControllerV2 _uiController;

    #endregion

    #endregion
    
    #region UnityFunctions

    private void Awake()
    {
        //Se encarga de verificar que mis scripts se encuentren en escena antes de cargar el resto de cosas, asi ya no es necesario colocarlo a mano
        #region Finders

        _dropController = FindFirstObjectByType<DropController>();
        if (_dropController == null)
        {
            Debug.LogError("DropController not Found in Scene");
        }

        _timer = FindFirstObjectByType<Timer>();
        if (_timer == null)
        {
            Debug.LogError("timer not Found in Scene");
        }
        
        _uiController = FindFirstObjectByType<UIControllerV2>();
        if (_timer == null)
        {
            Debug.LogError("UIControllerV2 not Found in Scene");
        }

        #endregion
        //Establece valores iniciales
        lastDropFrequency = dropFrequency;
        lastDropPerSecond = DropPerSecond;
    }

    private void Update()
    {
        GravitySwitch();
        SetSpeed();
        if (dropFrequency != lastDropFrequency)// Actualiza la última frecuencia
        {
            lastDropFrequency = dropFrequency; 
            SetTimer();
        }

        if (DropPerSecond != lastDropPerSecond)//Actualiza la cantidad de gotas
        {
            UpdateDropPerSecond(DropPerSecond); 
            //Debug.Log("dropPerSecond updated to: " + dropPerSecond);    
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

    public void UpdateDropPerSecond(float drops)
    {
        if (_dropController != null)
        {
            _dropController.SetDropPerSecond(drops);
            Debug.Log("Called SetDropPerSecond on Controller with value: " + drops);
        }
    }

    #endregion

    #region Timer

    // Ajusta el temporizador con la nueva duración
    private void SetTimer()
    {
        if (_timer != null)
        {
            if (dropFrequency > 0)
            {
                _timer.SetInterval(dropFrequency); // Usa la duración directamente sin necesidad de inverso
                Debug.Log("Timer duration set to: " + dropFrequency);
            }
            else
            {
                Debug.LogWarning("dropintergval es cero o negativo, el temporizador no se actualizará.");
            }
        }
    }

    #endregion
}
