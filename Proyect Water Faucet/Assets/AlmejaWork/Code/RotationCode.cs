using System;
using UnityEngine;

public class RotationCode : MonoBehaviour
{
    // Codigo meramente visual que hace girar las llaves del grifo con el slider
    [SerializeField] private float minRot = 0f;
    [SerializeField] private float maxRot = 360f;
    private float _currentRotation = 0f;
    private float _lastSliderValue = 0f;

    [SerializeField] GameObject lKey, rKey;
    private Vector3 _rotDir = Vector3.back;



    public void InitializeRotation(float initialSliderValue)
    {
        _lastSliderValue = initialSliderValue;
    }

    // Controla la dirección de giro en base al slider
    public void KeysRotation(float sliderValue)
    {
        float sliderDelta = sliderValue - _lastSliderValue;
        _lastSliderValue = sliderValue;

        float rotationDelta = sliderDelta * (maxRot - minRot);

        if (rotationDelta > 0)
        {
            RotateRight(rotationDelta);
        }
        else if (rotationDelta < 0)
        {
            RotateLeft(-rotationDelta);   
        }
    }
    
    // Fucnión que define la dirección que deberán seguir las llaves

    public void ApplyRotation()
    {
        rKey.transform.localRotation = Quaternion.Euler(-90,0,-_currentRotation);
        lKey.transform.localRotation = Quaternion.Euler(-90,0,_currentRotation);
    }
 
    //Formulas que permiten que la llave guire solo lo necesario junto con el slider
    private void RotateLeft(float angle)
    {
        _currentRotation -= angle;
        _currentRotation = Mathf.Clamp(_currentRotation, minRot, maxRot);
        ApplyRotation();
    }
    private void RotateRight(float angle)
    {
        _currentRotation += angle;
        _currentRotation = Mathf.Clamp(_currentRotation, minRot, maxRot);
        ApplyRotation();
    }
}
