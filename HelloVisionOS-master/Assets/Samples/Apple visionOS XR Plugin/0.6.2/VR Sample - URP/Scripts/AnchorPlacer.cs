using UnityEngine;

#if UNITY_EDITOR || UNITY_VISIONOS
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.VisionOS.InputDevices;
#endif

namespace UnityEngine.XR.VisionOS.Samples.URP
{
    public class AnchorPlacer : MonoBehaviour
    {
#if UNITY_EDITOR || UNITY_VISIONOS
        ARAnchor m_Anchor;
        PointerInput m_PointerInput;

        void OnEnable()
        {
            m_PointerInput ??= new PointerInput();
            m_PointerInput.Enable();
        }

        void OnDisable()
        {
            m_PointerInput.Disable();
        }

        void Update()
        {
            // Wait until session is ready and tracking
            if (ARSession.state < ARSessionState.SessionTracking)
            {
                return;
            }

            var primaryTouch = m_PointerInput.Default.PrimaryPointer.ReadValue<VisionOSSpatialPointerState>();
            if (primaryTouch.phase != VisionOSSpatialPointerPhase.Began)
                return;

            if (m_Anchor != null)
                Destroy(m_Anchor.gameObject);

            var anchorTransform = new GameObject($"Anchor {Time.time}").transform;
            var ray = new Ray(primaryTouch.startRayOrigin, primaryTouch.startRayDirection);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                anchorTransform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
            else
            {
                anchorTransform.SetPositionAndRotation(primaryTouch.devicePosition, primaryTouch.deviceRotation);
            }

            m_Anchor = anchorTransform.gameObject.AddComponent<ARAnchor>();
        }
#endif
    }
}
