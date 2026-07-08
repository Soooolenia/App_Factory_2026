using UnityEngine;

public class NagivationVisualManager : MonoBehaviour
{
    [SerializeField] private RadialUIAnimation[] radialUIIndications;
    [SerializeField] private IndicationAnimation[] straightIndications;
    private void Start()
    {
        radialUIIndications = GetComponentsInChildren<RadialUIAnimation>();
        straightIndications = GetComponentsInChildren<IndicationAnimation>();
    }
    public void StopUIAnimation()
    {
        foreach (RadialUIAnimation UI in radialUIIndications)
        {
            UI.Finish();
            UI.enabled = false;
        }
        foreach (IndicationAnimation UI in straightIndications)
        {
            UI.Finish();
            UI.enabled = false;
        }
    }
}
