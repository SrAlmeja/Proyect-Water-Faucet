using System;
using UnityEngine;

public class SplatteringController : MonoBehaviour
{
    [SerializeField] private ParticleSystem splatter;
    [SerializeField] private ParticleSystem splatter2;

    [Range(0.6f, 1f)] [SerializeField] private float splatterPower;
    [Range(0f, 0.3f)] [SerializeField] private float splatterPower2;
    [Range(0f, 0.05f)] [SerializeField] private float splatterLife = 0f;
    
    [SerializeField] private float limitToTurnOff;
    private bool isFirstParticleActive = true;

    #region Getters & Setters

    public float SplatterPower
    {
        get => splatterPower;
        set
        {
            splatterPower = Mathf.Clamp(value, 0.6f, 1f);
            HideSplatter();
        }
    }
    public float SplatterPower2
    {
        get => splatterPower2;
        set
        {
            splatterPower = Mathf.Clamp(value, 0f, 0.3f);
            HideSplatter();
        }
    }
    public float SplatterLife
    {
        get => splatterLife;
        set
        {
            splatterLife = Mathf.Clamp(value, 0f, 0.5f);
            HideSplatter();
        }
    }

    #endregion
    
    private void Start()
    {
        // La particula debe iniciar apagada
        splatter.Stop();
        splatter2.Stop();
    }

    public void HideSplatter()
    {
        // Determinamos la partícula activa y la configuramos
        if (isFirstParticleActive)
        {
            var mainModule = splatter.main;
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(splatterLife);
            var lVModule = splatter.limitVelocityOverLifetime;
            lVModule.dampen = splatterPower;

            if (splatterPower <= limitToTurnOff)
            {
                SwitcherOff(splatter);
            }
            else
            {
                SwitcherOn(splatter);
            }
        }
        else
        {
            var mainModule = splatter2.main;
            mainModule.startLifetime = new ParticleSystem.MinMaxCurve(splatterLife);
            var lVModule = splatter2.limitVelocityOverLifetime;
            lVModule.dampen = splatterPower2;

            if (splatterPower2 <= limitToTurnOff)
            {
                SwitcherOff(splatter2);
            }
            else
            {
                SwitcherOn(splatter2);
            }
        }
    }

    // Funciones de apagado/encendido
    private void SwitcherOff(ParticleSystem splatter)
    {
        splatter.Stop();
        // Debug.Log("Splatter off");
    }

    private void SwitcherOn(ParticleSystem splatter)
    {
        splatter.Play();
        // Debug.Log("Splatter on");
    }

    // Función para cambiar de partícula activa
    public void SwitchParticle()
    {
        isFirstParticleActive = !isFirstParticleActive;
        HideSplatter();
    }
}