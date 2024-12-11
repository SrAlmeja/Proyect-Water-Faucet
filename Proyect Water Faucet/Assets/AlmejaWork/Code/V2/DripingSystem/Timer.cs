using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Action OnTimerComplete;

    #region Variables
    
    [SerializeField] private float timerDuration;
    private float _currentTime;
    private bool _isCounting = false;
    [SerializeField] private SOBoolean isPaused;

    #endregion

    #region UnityFunctions

    private void Awake()
    {
        if (OnTimerComplete != null)
        {
            OnTimerComplete.Invoke();
        }

        ResetTimer();
    }

    private void Start()
    {
        StartCoroutine(Counting());
    }
    

    #endregion

    #region Timer Functions

    private IEnumerator Counting()
    {
        if (timerDuration <= 0)
        {
            Debug.LogWarning("El tiempo del temporizador es 0 o menor. El contador se detiene.");
            yield break;
        }
        
        _isCounting = true;
        _currentTime = timerDuration;
        
        
        while (_currentTime > 0)
        {
            if (isPaused.value)
            {
                yield return null;
                continue;
            }
            
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }

        _isCounting = false;
        OnTimerComplete?.Invoke();
        ResetTimer();
    }
    
    private void ResetTimer()
    {
        if (_isCounting)
        {
            StopCoroutine(Counting());
        }

        if (timerDuration > 0)
        {
            _currentTime = timerDuration;
            StartCoroutine(Counting());
        }

    }

    public void SetDuration(float duration)
    {
        timerDuration = duration;
        Debug.Log("Timer ajustado a " + duration);
        ResetTimer();
    }
    

    #endregion
    
}
