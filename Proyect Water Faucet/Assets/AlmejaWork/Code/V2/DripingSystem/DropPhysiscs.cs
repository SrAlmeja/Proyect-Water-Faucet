using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropPhysiscs : MonoBehaviour
{
    #region Variable

    [Header("MovementVar")]
    [SerializeField] private SOFloat speed;
    private float pSpeed = 0f;
    private Vector3 direction = Vector3.down;
    
    [Header ("StopConditional")]
    [SerializeField] private SOBoolean isPaused;

    [Header ("DistanceControll")]
    [SerializeField] private int dropToSpawn;
    [SerializeField] private float distanceBetweenDrops = 0.25f;
    
    #endregion

    #region Getters & Setters

    public float Speed
    {
        get => speed.value;
        set
        {
            speed.value = Mathf.Clamp(value, -1f, 1f);;
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
        if (isPaused.value)
        {
            Speed = pSpeed;
        }
        else
        {
            Speed = speed.value;
        }
    }
    
    private void Movement()
    {
        transform.Translate(direction * Speed * Time.fixedDeltaTime);
    }
    
}
 