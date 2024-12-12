using UnityEngine;

public class DrippingControllerV1 : MonoBehaviour
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
        //Se accede a l modulo de emission de la particula para poder modificarlo a gusto
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
    
    //Funciones para apagar/ la particula
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
