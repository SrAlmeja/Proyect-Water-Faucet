using System;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class DropController : MonoBehaviour
{
    #region Variables
    
    private Timer _timer;
    private float _dropPerSecond;
    private float _frequency;
    private float _distance = 1f;

    #endregion

    #region UnityFunction

    private void Awake()
    {
        _timer = FindFirstObjectByType<Timer>();
        if (_timer == null)
        {
            Debug.LogError("Timer not Found in Scene");
        }
    }

    #endregion
    
    #region DrippingLogic

    public void SetDropPerSecond(float drops)
    {
        _dropPerSecond = Mathf.Max(0.1f, drops); //Esto evitara valores negativos
        Debug.Log("SetDropPerSecond called. New dropPerSecond: " + _dropPerSecond);
        UpdateFrequency();
    }

    private void UpdateFrequency()
    {
        if (_dropPerSecond > 0 )
        {
            _frequency = _distance / _dropPerSecond;
            Debug.Log("Frequency updated: " + _frequency);
            if (_timer != null)
            {
                _timer.SetInterval(_frequency);
                Debug.Log("Timer interval set to: " + _frequency);
            }
        }
    }

    #endregion
    
}
