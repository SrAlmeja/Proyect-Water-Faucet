using System;
using UnityEngine;

public class SplatteringController : MonoBehaviour
{
    [SerializeField] private ParticleSystem splatter;

    [Range(0.6f, 1f)] [SerializeField] private float splatterPower;
    [Range(0f, 0.05f)] [SerializeField] private float splatterLife = 0f;
    
    [SerializeField] private float limitToTurnOff;

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
        splatter.Stop();
    }

    public void HideSplatter()
    {
        var mainModule = splatter.main;
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(splatterLife);
        var lVModule = splatter.limitVelocityOverLifetime;
        lVModule.dampen = splatterPower;

        if (splatterPower <= limitToTurnOff)
        {
            SwitcherOff();
        }
        else
        {
            SwitcherOn();
        }
    } 
    
    private void SwitcherOff()
    {
        splatter.Stop();
        //Debug.Log("splatter off");
    }
    private void SwitcherOn()
    {
        splatter.Play();
        //Debug.Log("Splatter on");
    }
    
}
