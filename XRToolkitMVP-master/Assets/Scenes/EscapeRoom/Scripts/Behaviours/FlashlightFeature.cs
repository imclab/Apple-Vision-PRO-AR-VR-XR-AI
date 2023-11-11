using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightFeature : BaseFeature
{
    [Header("Flashlight configuration")]
    [SerializeField]
    private Transform flashlightPivot;

    [SerializeField]
    private bool on = false;

    [Header("Interaction Configuration")]
    [SerializeField]
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable?.activated.AddListener((s) =>
        {
            ToggleFlashlight();
        });
    }

    private void ToggleFlashlight()
    {
        on = !on;
        flashlightPivot.GetComponentInChildren<Light>().enabled = on;
        if (on)
        {
            PlayOnEnded();
        }
        else
        {
            PlayOnEnded();
        }
    }
}
