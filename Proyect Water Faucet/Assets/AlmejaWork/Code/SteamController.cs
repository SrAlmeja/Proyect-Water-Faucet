using System;
using UnityEngine;

public class SteamController : MonoBehaviour
{
    [SerializeField] private ParticleSystem steam;

    [Range(0,80)]
    [SerializeField] private float steamHider = 0f;
    
    public float SteamHider
    {
        get => steamHider;
        set
        {
            steamHider = Mathf.Clamp(value, 0f, 80f);
            HideSteam();
        }
    }
    

    public void HideSteam()
    {
        var mainModule = steam.main;
        mainModule.startColor = new Color(1f, 1f, 1f, steamHider);
        if (steamHider <= 14)
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
        steam.Stop();
        Debug.Log("Steam off");
    }
    private void SwitcherOn()
    {
        steam.Play();
        Debug.Log("Steam on");
    }
}
