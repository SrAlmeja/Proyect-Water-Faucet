using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    #region Variables

    public Action OnTimerComplete;
    
    [Header("TimerSettings")]
    [SerializeField] private float timerInterval = 1f;
    [SerializeField] private SOBoolean isPaused;
    private bool _isCero;
    private float _currentTime;

    #endregion

    #region UnityFunctions
    
    private void Start()
    {
        SetInterval(timerInterval);
        Debug.Log("Timer started with interval: " + timerInterval);
    }

    private void Update()
    {
        ClockFunction();
    }

    #endregion
    
    #region Clock Functions
    
    public void SetInterval(float interval)
    {
        _isCero = false;
        timerInterval = interval;
        _currentTime = timerInterval;
        Debug.Log("Timer interval set to: " + interval);
    }
    private void ClockFunction()
    {
        
        
        if (isPaused.value || _isCero)
        {
            return;
        }
        else
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                OnTimerComplete?.Invoke();
                _currentTime = timerInterval; //Resetea el timer
                #region Debuggers

                Debug.Log("Timer completed and event invoked.");
                //Debug.Log("Timer reset to interval: " + 
                Debug.Log("isCero =" + _isCero);
                //Debug.Log("Timer: " + _currentTime);
                #endregion 
            }
        }
    }
    public void IntervalCero()
    {
        _isCero = true;
    }
    
    #endregion
}