using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeature : BaseFeature
{
    [Header("Drawer Configuration")]
    [SerializeField]
    private Transform drawerPivot;

    [SerializeField]
    private float maxDistance = 0;

    [SerializeField]
    private FeatureDirection direction = FeatureDirection.Forward;


    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private bool open = false;

    [Header("Interaction Configuration")]
    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        // doors with simple selections (no sockets)
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            if (!open)
            {
                OpenDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        open = true;
        StartCoroutine(ProcessMotion());
        PlayOnStarted();
    }

    private IEnumerator ProcessMotion()
    { 
        while(open)
        {
            if (direction == FeatureDirection.Forward && drawerPivot.localPosition.z <= maxDistance)
            {
                drawerPivot.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (direction == FeatureDirection.Backward && drawerPivot.localPosition.z >= maxDistance)
            {
                drawerPivot.Translate(-Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                open = false;
            }
            yield return null;
        }
    }
}
