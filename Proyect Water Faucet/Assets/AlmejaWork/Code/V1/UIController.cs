using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region WaterController

    [SerializeField] private Slider waterController;
    [SerializeField] private Material waterMat;

    [SerializeField] private float minClipValue, maxClipValue;

    #endregion

    #region RotationController

    [SerializeField] private RotationCode rotationScript;

    #endregion
    
    #region ParticleController

    [SerializeField] private DrippingController drippingController;
    [SerializeField] private SteamController steamController;
    [SerializeField] private SplatteringController splatteringController;

    #endregion

    private void Awake()
    {
        ValueSetter();
    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        
        waterController.onValueChanged.AddListener(UpdateDripVisivility);
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
    void UpdateClipValue(float value)
    {
        if (waterMat != null)
        {
            waterMat.SetFloat("_Clip", value);
            waterMat.SetFloat("MainTexPower", value);
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

    void UpdateDripVisivility(float value)
    {
        float dripValueHider = Mathf.Lerp(4f, 12f, Mathf.InverseLerp(0f, 0.11f, value));
        drippingController.DripFrequence = dripValueHider;    
    }
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
