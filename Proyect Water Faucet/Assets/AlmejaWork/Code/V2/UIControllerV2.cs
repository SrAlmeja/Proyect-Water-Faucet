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

    [SerializeField] private float minClipValue = 0f, maxClipValue = 12f;
    [SerializeField] private SOBoolean isPaused;

    [Header("Controllers")]
    [SerializeField] private DrippingController _drippingController;
    
    #endregion

    #region RotationController

    [SerializeField] private RotationCode rotationScript;

    #endregion
    
    #region ParticleController
    
    [SerializeField] private SteamController steamController;
    [SerializeField] private SplatteringController splatteringController;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        ValueSetter();

        _drippingController = FindFirstObjectByType<DrippingController>();
        if (_drippingController == null)
        {
            Debug.LogError("DrippingController not Found in Scene");
        }

    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        
        Listeners();
        
        rotationScript.InitializeRotation(waterController.value);
        
    }
    
    public void TogglePause()
    {
        isPaused.value = !isPaused.value;
    }

    #endregion
    


    
   
    //Formulas de interpolacion para que los valores coincidan con el de los slider
    #region ClipFunctions

    private void Listeners()
    {
        waterController.onValueChanged.AddListener(Pause);
        waterController.onValueChanged.AddListener(UpdateClipValue);
        waterController.onValueChanged.AddListener(UpdateRotationSpeed);
        waterController.onValueChanged.AddListener(UpdateSteamVisivility);
        waterController.onValueChanged.AddListener(UpdateSplatteringVisivility);
        waterController.onValueChanged.AddListener(UpdateDrippingController);
    }
    
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

    private void UpdateDrippingController(float value)
    {
        if (_drippingController != null) { _drippingController.UpdateDropPerSecond(value); }
    }
}
