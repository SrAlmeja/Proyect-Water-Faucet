using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropPhysics : MonoBehaviour
{
    #region Variable

    [Header("MovementVar")]
    [SerializeField] private SOFloat speed;
    private float pSpeed = 0f;
    private Vector3 direction = Vector3.down;
    
    [Header ("StopConditional")]
    [SerializeField] private SOBoolean isPaused;

    [Header("Collision")]
    [SerializeField] private ParticleSystem splatteringParticleSystem;
    private DropSpawner _dropSpawner;
    
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

    #region UnityMethods

    private void Awake()
    {
        #region Finders

        _dropSpawner = FindFirstObjectByType<DropSpawner>();
        if (_dropSpawner == null)
        {
            Debug.LogError("Drop");
        }

        #endregion
    }

    private void FixedUpdate()
    {
        UpdateSpeed();
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (splatteringParticleSystem != null)
            {
                // Particle Instantiation
                ParticleSystem splatteringParticle = Instantiate(splatteringParticleSystem,
                    transform.position, Quaternion.identity);
                
                // Playing the Particle
                splatteringParticle.Play();
                Destroy(splatteringParticle.gameObject, splatteringParticle.main.duration);
            }
            _dropSpawner.DestroyDrop(transform.gameObject);
        }
    }

    #endregion

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
 