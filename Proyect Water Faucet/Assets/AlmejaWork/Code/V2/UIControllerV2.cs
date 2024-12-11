using System;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerV2 : MonoBehaviour
{
    #region Pause

    private bool _isPaused;

    #endregion
    
    #region WaterController

    [SerializeField] private Slider waterController;
    [SerializeField] private Material waterMat;

    [HideInInspector][SerializeField] private float minClipValue = 0f, maxClipValue = 8f;

    #endregion

    #region RotationController

    [SerializeField] private RotationCode rotationScript;

    #endregion
    
    #region ParticleController
    
    [SerializeField] private SteamController steamController;
    [SerializeField] private SplatteringController splatteringController;

    #endregion

    #region Get&Set

    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            IsPaused = _isPaused;
        }
    }

    #endregion

    private void Awake()
    {
        ValueSetter();
    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        
        waterController.onValueChanged.AddListener(Pause);
        waterController.onValueChanged.AddListener(UpdateClipValue);
        waterController.onValueChanged.AddListener(UpdateRotationSpeed);
        waterController.onValueChanged.AddListener(UpdateSteamVisivility);
        waterController.onValueChanged.AddListener(UpdateSplatteringVisivility);

        rotationScript.InitializeRotation(waterController.value);
        
        
    }

    
   
    //Formulas de interpolacion para que los valores coincidan con el de los slider
    #region ClipFunctions

    private void ValueSetter()
    {
        waterController.minValue = minClipValue;
        waterController.maxValue = maxClipValue;
    }
    
    #region PauseFunction

    
    
    void UpdateClipValue(float value)
    {
        // Clampear el valor del slider entre el rango definido de 0 a 8
        float clampedValue = Mathf.Clamp(value, (int)minClipValue, (int)maxClipValue);

        if (waterMat != null)
        {
            waterMat.SetFloat("_Clip", clampedValue);
            waterMat.SetFloat("MainTexPower", clampedValue);
        }
    }

    #endregion
    
    void Pause(float value)
    {
        if (value == 0f)
        {
            _isPaused = true;
        }
        else
        {
            _isPaused = false;
        }
    }

    #endregion
    
    void UpdateRotationSpeed(float value)
    {
        if (rotationScript != null)
        {
            rotationScript.KeysRotation(value);
        }
    }
    

    #region ParticleFunctions
    
    void UpdateSteamVisivility(float value)
    {
        float steamValueHider = Mathf.Lerp(0f, 80f, Mathf.InverseLerp(0f, 0.6f, value));
        steamController.SteamHider = steamValueHider;    
    }
    
    void UpdateSplatteringVisivility(float value)
    {
        float splatterValueLife = Mathf.Lerp(0f, 0.05f, Mathf.InverseLerp(0f, 0.6f, value));
        splatteringController.SplatterLife = splatterValueLife;
        float splatterValuePower = Mathf.Lerp(1f, 0.6f, Mathf.InverseLerp(0f, 0.6f, value)); //Se invierte para lograr el efecto visual deseado
        splatteringController.SplatterPower = splatterValuePower;
    }

    #endregion

    
}
