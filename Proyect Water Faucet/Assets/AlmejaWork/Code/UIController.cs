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

    #region MyRegion

    

    #endregion

    private void Awake()
    {
        ValueSetter();
    }
    
    void Start()
    {
        waterController.value = waterMat.GetFloat("_Clip");
        waterController.onValueChanged.AddListener(UpdateClipValue);
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
    
}
