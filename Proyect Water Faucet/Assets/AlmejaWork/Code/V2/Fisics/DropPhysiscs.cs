using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPhysiscs : MonoBehaviour
{
    #region MyRegion

    [SerializeField] private float speed, sSpeed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private SOBoolean isPause;
    
    #endregion


    private void FixedUpdate()
    {
        Gravity();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    private void Stop()
    {
        transform.Translate(direction * sSpeed * Time.fixedDeltaTime);
    }

    private void Gravity()
    {
        if (isPause.value != true)
        {
            Move();
            Debug.Log("Estoy en callendo");
        }
        else
        {
            Stop();
            Debug.Log("Estoy en Pausa");
        }
    }
}
 