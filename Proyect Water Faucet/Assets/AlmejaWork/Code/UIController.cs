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

    [SerializeField] private SteamController steamController; 

    #endregion

    private void Awake()
    {
        ValueSetter();
    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        
        waterController.onValueChanged.AddListener(UpdateClipValue);
        waterController.onValueChanged.AddListener(UpdateRotationSpeed);
        waterController.onValueChanged.AddListener(UpdateSteamVisivility);

        rotationScript.InitializeRotation(waterController.value);
        
        
    }

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

    void UpdateSteamVisivility(float value)
    {
        float steamHiderValue = Mathf.Lerp(0f, 80f, Mathf.InverseLerp(0f, 0.6f, value));
        steamController.SteamHider = steamHiderValue;    
    }
    
}
