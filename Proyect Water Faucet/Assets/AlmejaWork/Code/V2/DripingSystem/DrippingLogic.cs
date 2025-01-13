using UnityEngine;

public class DrippingLogic : MonoBehaviour
{
    #region Variables

    [Header("Dripping Settings")]
    [SerializeField] private int dropMult;
    private float _dropPerSecond;
    private float _lastDropPerSecond;
    private float _distance = 1f;
    
    [Header("Timer Settings")]
    [SerializeField] private Timer timer;
    private float _dropFrequency;
    private float _lastDropFrequency;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
