using UnityEngine;

public class DrippingController : MonoBehaviour
{
    [SerializeField] private ParticleSystem drip;
    [Range(4f, 12f)] [SerializeField] private float dripFrequence;
    
    [SerializeField] private float limitToTurnOff;
    
    #region Getters & Setters

    public float DripFrequence
    {
        get => dripFrequence;
        set
        {
            dripFrequence = Mathf.Clamp(value, 4f, 12f);
            HideDrip();
        }
    }

    #endregion
    
    public void HideDrip()
    {
        var emissionModule = drip.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(dripFrequence);

        if (dripFrequence >= limitToTurnOff)
        {
            SwitcherOff();
        }
        else
        {
            SwitcherOn();
        }
    } 
    
    private void SwitcherOff()
    {
        drip.Stop();
        //Debug.Log("drip off");
    }
    private void SwitcherOn()
    {
        drip.Play();
        //Debug.Log("Drip on");
    }
}
