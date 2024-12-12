using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Action OnTimerComplete;

    #region Variables

    [SerializeField] private float timerInterval = 1f;
    [SerializeField] private SOBoolean isPaused;
    private float _currentTime;

    #endregion

    #region UnityFunctions
    
    private void Start()
    {
        _currentTime = timerInterval;
    }

    private void Update()
    {
        if (isPaused.value)
        {
            return;
        }

        _currentTime -= Time.deltaTime;
        if (_currentTime >= 0)
        {
            OnTimerComplete?.Invoke();
            _currentTime = timerInterval; //Resetea el timer
        }
    }

    #endregion

    #region Timer Functions

    public void SetInterval(float interval)
    {
        timerInterval = interval;
        _currentTime = timerInterval; 
    }

   #endregion
}
