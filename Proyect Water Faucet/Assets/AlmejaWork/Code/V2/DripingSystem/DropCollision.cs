using System;
using UnityEngine;

public class DropCollision : MonoBehaviour
{
    #region Variables

    private DropSpawner _dropSpawner; 
    [SerializeField] private ParticleSystem splatteringParticlePrefab;

    #endregion

    private void Awake()
    {
        #region Finders
        
        _dropSpawner = GameObject.FindObjectOfType<DropSpawner>();
        if (_dropSpawner == null)
        {
            Debug.LogError("DropSpawner not Found in Scene");
        }


        #endregion
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            if (splatteringParticlePrefab != null)
            {
                // Instanciamos la partícula en la posición de la colisión
                ParticleSystem splatteringParticle = Instantiate(splatteringParticlePrefab, transform.position, Quaternion.identity);

                // Reproducir la partícula instanciada
                splatteringParticle.Play();
                Destroy(splatteringParticle.gameObject, splatteringParticle.main.duration); // El tiempo de vida de la partícula es igual a la duración del sistema de partículas

            }
            _dropSpawner.DestroyDrop(transform.gameObject); 
        }
    }
    
}
