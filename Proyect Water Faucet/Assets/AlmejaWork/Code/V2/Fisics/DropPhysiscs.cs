using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPhysiscs : MonoBehaviour
{
    #region Variable

    [Header("MovementVar")]
    [SerializeField] private float speed, pSpeed = 0f;
    private Vector3 direction = Vector3.down;
    [Header ("StopConditional")]
    [SerializeField] private SOBoolean isPaused;
    
    #endregion

    #region Getters & Setters

    public float Speed
    {
        get => speed;
        set
        {
            speed = Mathf.Clamp(value, -1f, 1f);;
        }
    }
    
    #endregion
    
    private void FixedUpdate()
    {
        UpdateSpeed();
        Movement();
    }

    
    //Iguala la velocidad a 0 para la pausa
    private void UpdateSpeed()
    {
        Speed = isPaused.value ? pSpeed : speed;   
    }
    
    private void Movement()
    {
        transform.Translate(direction * Speed * Time.fixedDeltaTime);
    }
}
 