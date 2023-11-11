using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeature : BaseFeature
{
    [Header("Door Configuration")]
    [SerializeField]
    private Transform doorPivot;

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool makeItKinematicOnceOpened;

    public bool Open 
    {
        get => open;
        set => open = value;
    }

    [Header("Interaction Configuration")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        // doors with sockets
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });

        socketInteractor?.selectExited.AddListener((s) =>
        {
            PlayOnEnded();
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true;
        });

        // doors with simple selections (no sockets)
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    public void OpenDoor()
    {
        if (!open)
        {
            open = true;
            StartCoroutine(ProcessMotion());
            PlayOnStarted();
        }
    }

    private IEnumerator ProcessMotion()
    { 
        while(open)
        {
            var angle = doorPivot.localEulerAngles.y < 180 ? doorPivot.localEulerAngles.y :
                doorPivot.localEulerAngles.y - 360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if (open && angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, speed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }
            else
            {
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();
                if(featureRigidBody != null && makeItKinematicOnceOpened) 
                featureRigidBody.isKinematic = true;
            }
            yield return null;
        }
    }
}
