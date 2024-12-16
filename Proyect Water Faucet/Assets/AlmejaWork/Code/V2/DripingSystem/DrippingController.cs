using System;
using UnityEngine;

public class DrippingController : MonoBehaviour
{
    #region Variables

    //[Header("Testing")] [SerializeField] private bool ispaused;

    [Header("Dripping Settings")]
    [SerializeField] private int dropMult = 1;
    [HideInInspector] public float DropPerSecond = 1f;
    private float _lastDropPerSecond;
    private float _distance = 1f;
    
    [Header("Timer Settings")]
    [SerializeField] private Timer timer;
    private float _dropFrequency;
    private float _lastDropFrequency;

    [Header("DropSettings")]
    [SerializeField] private float dSpeed;
    [SerializeField] private SOFloat dropSpeed;

    #endregion

    #region UnityFunctions

    private void Awake()
    {
        SetDropsPerSecond(DropPerSecond, dropMult);
        
        _lastDropFrequency = _dropFrequency;
        _lastDropPerSecond = DropPerSecond;
        
        
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

        if (DropPerSecond != _lastDropPerSecond)
        {
            SetDropsPerSecond(DropPerSecond, dropMult);
            _lastDropPerSecond = DropPerSecond;
            Debug.Log("dropPerSecond ah cambiado: " + DropPerSecond);    
        }
    }

    #endregion
    
    #region SpawnerFunctions

    public void SetDropsPerSecond(float newDropsPerSecond, int mult)
    {
        DropPerSecond = Mathf.Max(0f, (newDropsPerSecond * mult));
        //Debug.Log("Actual DropsPerSecond = " + _dropPerSecond);
        UpdateFrequency();
    }
    
    private void UpdateFrequency()
    {
        if (DropPerSecond > 0) 
        { _dropFrequency = _distance / DropPerSecond; 
            //Debug.Log("Frequency updated: " + _dropFrequency);
            if (timer != null)
            {
                timer.SetInterval(_dropFrequency);
                //Debug.Log("Timer interval set to: " + _dropFrequency);
            }
        }
        else if (DropPerSecond == 0)
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

