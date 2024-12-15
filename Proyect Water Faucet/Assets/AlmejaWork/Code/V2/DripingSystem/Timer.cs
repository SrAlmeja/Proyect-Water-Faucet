using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Action OnTimerComplete;

    #region Variables

    [Header("TimerSettings")]
    [SerializeField] private float timerInterval = 1f;
    [SerializeField] private SOBoolean isPaused;
    private bool isCero;
    private float _currentTime;

    #endregion

    #region UnityFunctions
    
    private void Start()
    {
        _currentTime = timerInterval;
        Debug.Log("Timer started with interval: " + timerInterval);
    }

    private void Update()
    {
        Debug.Log("isCero =" + isCero);
        if (isPaused.value || isCero)
        {
            return;
        }

        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0)
        {
            OnTimerComplete?.Invoke();
            //Debug.Log("Timer completed and event invoked.");
            _currentTime = timerInterval; //Resetea el timer
            //Debug.Log("Timer reset to interval: " + 
            Debug.Log("Timer: " + _currentTime);
        }
    }

    #endregion

    #region Timer Functions

    public void SetInterval(float interval)
    {
        isCero = false;
        timerInterval = interval;
        _currentTime = timerInterval;
        //Debug.Log("Timer interval set to: " + interval);
        
    }

    public void IntervalCero()
    {
        isCero = true;
        
    }

   #endregion
}