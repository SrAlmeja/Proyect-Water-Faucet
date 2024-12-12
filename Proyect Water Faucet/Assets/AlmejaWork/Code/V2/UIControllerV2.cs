using System;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerV2 : MonoBehaviour
{
    #region Variables

    #region WaterController

    [Header("SliderValues")]
    [SerializeField] private Slider waterController;
    private bool _isPaused;
    [Header("WaterMaterial")]
    [SerializeField] private Material waterMat;
    [SerializeField] private float minClipValue = 0f, maxClipValue = 8f;

    private DrippingController _drippingController;

    #endregion

    #region RotationController

    [SerializeField] private RotationCode rotationScript;

    #endregion
    
    #region ParticleController
    
    [SerializeField] private SteamController steamController;
    [SerializeField] private SplatteringController splatteringController;

    #endregion

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

    #region UnityVariables

    private void Awake()
    {
        ValueSetter();

        #region Finders
        
        _drippingController = FindFirstObjectByType<DrippingController>();
        if (_drippingController == null)
        {
            Debug.LogError("DrippingController not Found in Scene");
        }

        #endregion
    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        
        waterController.onValueChanged.AddListener(Pause);
        waterController.onValueChanged.AddListener(UpdateClipValue);
        waterController.onValueChanged.AddListener(UpdateRotationSpeed);
        waterController.onValueChanged.AddListener(UpdateDrippingController);
        waterController.onValueChanged.AddListener(UpdateSteamVisivility);
        waterController.onValueChanged.AddListener(UpdateSplatteringVisivility);
        
        rotationScript.InitializeRotation(waterController.value);
    }

    #endregion

    #region ControllerLogic

    //Formulas de interpolacion para que los valores coincidan con el de los slider
    #region Slider & Material Functions

    private void ValueSetter()
    {
        waterController.minValue = minClipValue;
        waterController.maxValue = maxClipValue;
    }
    
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
    
    #region PauseFunction

    void Pause(float value)
    {
        _isPaused = value == 0f;
    }

    #endregion

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

    private void UpdateDrippingController(float value)
    {
        if (_drippingController != null)
        {
            _drippingController.UpdateDropPerSecond(value);
        }
    }

    void UpdateRotationSpeed(float value)
    {
        if (rotationScript != null)
        {
            rotationScript.KeysRotation(value);
        }
    }

    #endregion 
    
}
