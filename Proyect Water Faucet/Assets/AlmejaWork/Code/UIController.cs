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

    [SerializeField] private RotationCode rotationScript; // Referencia al script de rotación

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

        rotationScript.InitializeRotation(waterController.value);
    }

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
    
    void UpdateRotationSpeed(float value)
    {
        // Ajusta la velocidad de rotación en el script de rotación
        if (rotationScript != null)
        {
            rotationScript.KeysRotation(value);
        }
    }
}
