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
        if (isPaused.value)
        {
            return;
        }

        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0)
        {
            OnTimerComplete?.Invoke();
            Debug.Log("Timer completed and event invoked.");
            _currentTime = timerInterval; //Resetea el timer
            Debug.Log("Timer reset to interval: " + timerInterval);
        }
    }

    #endregion

    #region Timer Functions

    public void SetInterval(float interval)
    {
        timerInterval = interval;
        _currentTime = timerInterval;
        Debug.Log("Timer interval set to: " + interval);
    }

   #endregion
}