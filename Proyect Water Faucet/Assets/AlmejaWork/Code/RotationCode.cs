using System;
using UnityEngine;

public class RotationCode : MonoBehaviour
{
    [HideInInspector] [SerializeField] private float rotationSpeed;
    private Vector3 _rotDir = Vector3.back;
    [SerializeField] private bool lKey;

    
// Defining the rot Direction
    
    public void HandleInput()
    {
        if (!lKey)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
    }

    // The Stetic rotation of the keys
    private void RotateLeft()
    {
        transform.Rotate(_rotDir * -rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void RotateRight()
    {
        transform.Rotate(_rotDir * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
