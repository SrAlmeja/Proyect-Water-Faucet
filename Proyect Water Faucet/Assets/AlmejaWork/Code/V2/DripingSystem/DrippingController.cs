using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables

    //[Header("Testing")] [SerializeField] private bool ispaused;

    [Header("Dripping Settings")]

    [SerializeField] private float _dropPerSecond;
    [SerializeField] private float _dropFrequency;
    [SerializeField] private DropSpawner dropSpawner;
    
    private float _lastDropPerSecond;
    private float _lastDropFrequency;
    private float _distance = 1f;
    
    [Header("Timer Settings")]
    [SerializeField] private Timer timer;

    [Header("DropSettings")]
    [SerializeField] private float dSpeed;
    [SerializeField] private SOFloat dropSpeed;
    
    [Header("Pause Settings")] [SerializeField]
    private SOBoolean isPausedScriptable;

    #endregion

    #region UnityFunctions

    private void Awake()
    {
        _lastDropFrequency = _dropFrequency;
        _lastDropPerSecond = _dropPerSecond;

        if (timer == null)
        {
            Debug.LogError("timer not Found in Scene");
        }
        else
        {
            UpdateFrequency(); 
        }
    }

    private void Update()
    {
        SetSpeed();
        if (_dropFrequency != _lastDropFrequency)
        {
            _lastDropFrequency = _dropFrequency; // Actualiza la última frecuencia
        }

        if (_dropPerSecond != _lastDropPerSecond)
        {
            SetDropsPerSecond(_dropPerSecond);
            _lastDropPerSecond = _dropPerSecond;
            Debug.Log("dropPerSecond ah cambiado: " + _dropPerSecond);    
        }
    }

    #endregion
    
    #region SpawnerFunctions

    public void SetDropsPerSecond(float newDropsPerSecond)
    {
        _dropPerSecond = Mathf.Max(0f, newDropsPerSecond);
        //Debug.Log("Actual DropsPerSecond = " + _dropPerSecond);
        UpdateFrequency();
    }
    
    private void UpdateFrequency()
    {
        if (_dropPerSecond > 0) 
        { _dropFrequency = _distance / _dropPerSecond; 
            //Debug.Log("Frequency updated: " + _dropFrequency);
            if (timer != null)
            {
                timer.SetInterval(_dropFrequency);
                //Debug.Log("Timer interval set to: " + _dropFrequency);
            }
        }
        else if (_dropPerSecond == 0)
        {
            timer.IntervalCero();
        }
    } 

    #endregion
    
    #region DropPhysics

    private void SetSpeed()
    {
        dropSpeed.value = dSpeed;
    }
    
    #endregion
}

