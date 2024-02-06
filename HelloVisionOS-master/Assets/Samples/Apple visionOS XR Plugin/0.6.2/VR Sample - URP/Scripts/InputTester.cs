using System;
using UnityEngine;

#if UNITY_EDITOR || UNITY_VISIONOS
using UnityEngine.XR.VisionOS.InputDevices;
#endif

namespace UnityEngine.XR.VisionOS.Samples.URP
{
    public class InputTester : MonoBehaviour
    {
        [SerializeField]
        Transform m_Device;

        [SerializeField]
        Transform m_Ray;

        [SerializeField]
        Transform m_Target;

#if UNITY_EDITOR || UNITY_VISIONOS
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
            var primaryTouch = m_PointerInput.Default.PrimaryPointer.ReadValue<VisionOSSpatialPointerState>();
            var phase = primaryTouch.phase;
            var began = phase == VisionOSSpatialPointerPhase.Began;
            var active = began || phase == VisionOSSpatialPointerPhase.Moved;
            m_Device.gameObject.SetActive(active);
            m_Ray.gameObject.SetActive(active);

            if (began)
            {
                var rayOrigin = primaryTouch.startRayOrigin;
                var rayDirection = primaryTouch.startRayDirection;
                m_Ray.position = rayOrigin;
                m_Ray.rotation = Quaternion.LookRotation(rayDirection);

                var ray = new Ray(rayOrigin, rayDirection);
                var hit = Physics.Raycast(ray, out var hitInfo);
                m_Target.gameObject.SetActive(hit);
                m_Target.position = hitInfo.point;
            }

            if (active)
            {
                m_Device.position = primaryTouch.devicePosition;
                m_Device.rotation = primaryTouch.deviceRotation;
            }
        }
#endif
    }
}
