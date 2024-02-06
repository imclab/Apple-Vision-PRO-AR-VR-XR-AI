using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace PolySpatial.Samples
{
    public class InputDebuggingHelper : MonoBehaviour
    {
        [SerializeField]
        List<DebugTouchHelper> m_Touches;

        [SerializeField]
        InputActionReference m_TouchZeroValue;

        [SerializeField]
        InputActionReference m_TouchOneValue;

        [SerializeField]
        InputActionReference m_TouchZeroPhase;

        [SerializeField]
        InputActionReference m_TouchOnePhase;

        [SerializeField]
        Transform m_TouchZeroInputGizmo;

        [SerializeField]
        Transform m_TouchOneInputGizmo;

        [SerializeField]
        TextMeshPro m_ActiveTouches;

        [SerializeField]
        Transform m_TouchZeroDeviceInputGizmo;

        [SerializeField]
        Transform m_TouchOneDeviceInputGizmo;

        [SerializeField]
        Transform m_TouchZeroRay;

        [SerializeField]
        Transform m_TouchOneRay;

        const string k_FloatFormat = "00.00";

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            m_TouchZeroValue.action.Enable();
            m_TouchZeroPhase.action.Enable();
            m_TouchOneValue.action.Enable();
            m_TouchOnePhase.action.Enable();
        }

        void Update()
        {
            m_ActiveTouches.text = $"Active Touches: {Touch.activeTouches.Count}";

            // get values
            var touchZeroValue = m_TouchZeroValue.action.ReadValue<SpatialPointerState>();
            var touchZeroPhase = m_TouchZeroPhase.action.ReadValue<TouchPhase>();

            var touchOneValue = m_TouchOneValue.action.ReadValue<SpatialPointerState>();
            var touchOnePhase = m_TouchOnePhase.action.ReadValue<TouchPhase>();

            // set UI values
            SetTextValues(m_Touches[0], touchZeroValue, touchZeroPhase);
            SetTextValues(m_Touches[1], touchOneValue, touchOnePhase);

            // show input visualization
            if (touchZeroPhase == TouchPhase.Began || touchZeroPhase == TouchPhase.Moved)
            {
                m_TouchZeroInputGizmo.position = touchZeroValue.interactionPosition;
                m_TouchZeroInputGizmo.rotation = touchZeroValue.deviceRotation;
                m_TouchZeroDeviceInputGizmo.position = touchZeroValue.devicePosition;
                m_TouchZeroDeviceInputGizmo.rotation = touchZeroValue.deviceRotation;

                // Switch to unbounded mode to get selection ray data
                var rayDirection = touchZeroValue.startInteractionRayDirection;
                var hasSelectionRay = rayDirection != Vector3.zero;
                m_TouchZeroRay.gameObject.SetActive(hasSelectionRay);
                if (hasSelectionRay)
                {
                    m_TouchZeroRay.position = touchZeroValue.startInteractionRayOrigin;
                    m_TouchZeroRay.rotation = Quaternion.LookRotation(rayDirection);
                }
            }

            if (touchOnePhase == TouchPhase.Began || touchOnePhase == TouchPhase.Moved)
            {
                m_TouchOneInputGizmo.position = touchOneValue.interactionPosition;
                m_TouchOneInputGizmo.rotation = touchOneValue.deviceRotation;
                m_TouchOneDeviceInputGizmo.position = touchOneValue.devicePosition;
                m_TouchOneDeviceInputGizmo.rotation = touchOneValue.deviceRotation;

                // Switch to unbounded mode to get selection ray data
                var rayDirection = touchOneValue.startInteractionRayDirection;
                var hasSelectionRay = rayDirection != Vector3.zero;
                m_TouchOneRay.gameObject.SetActive(hasSelectionRay);
                if (hasSelectionRay)
                {
                    m_TouchOneRay.position = touchOneValue.startInteractionRayOrigin;
                    m_TouchOneRay.rotation = Quaternion.LookRotation(rayDirection);
                }
            }
        }

        void SetTextValues(DebugTouchHelper debugTouch, SpatialPointerState touchState, TouchPhase touchPhase)
        {
            var touchPosition = touchState.interactionPosition;
            var touchDevicePosition = touchState.devicePosition;
            var touchDeviceRotation = touchState.deviceRotation.eulerAngles;
            var touchSelectionRayPosition = touchState.startInteractionRayOrigin;
            var touchSelectionRayDirection = touchState.startInteractionRayDirection;

            debugTouch.InputTypeValue.text = touchState.Kind.ToString();
            debugTouch.PhaseValue.text = touchPhase.ToString();
            debugTouch.ColliderIDValue.text = touchState.targetObject != null ? touchState.targetObject.name : "None";
            debugTouch.XValue.text = touchPosition.x.ToString(k_FloatFormat);
            debugTouch.YValue.text = touchPosition.y.ToString(k_FloatFormat);
            debugTouch.ZValue.text = touchPosition.z.ToString(k_FloatFormat);
            debugTouch.DevicePositionXValue.text = touchDevicePosition.x.ToString(k_FloatFormat);
            debugTouch.DevicePositionYValue.text = touchDevicePosition.y.ToString(k_FloatFormat);
            debugTouch.DevicePositionZValue.text = touchDevicePosition.z.ToString(k_FloatFormat);
            debugTouch.DeviceRotationXValue.text = touchDeviceRotation.x.ToString(k_FloatFormat);
            debugTouch.DeviceRotationYValue.text = touchDeviceRotation.y.ToString(k_FloatFormat);
            debugTouch.DeviceRotationZValue.text = touchDeviceRotation.z.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionXValue.text = touchSelectionRayPosition.x.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionYValue.text = touchSelectionRayPosition.y.ToString(k_FloatFormat);
            debugTouch.SelectionRayPositionZValue.text = touchSelectionRayPosition.z.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationXValue.text = touchSelectionRayDirection.x.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationYValue.text = touchSelectionRayDirection.y.ToString(k_FloatFormat);
            debugTouch.SelectionRayRotationZValue.text = touchSelectionRayDirection.z.ToString(k_FloatFormat);
        }
    }

    [Serializable]
    public struct DebugTouchHelper
    {
        public TMP_Text InputTypeValue;

        public TMP_Text PhaseValue;

        public TMP_Text ColliderIDValue;

        public TMP_Text XValue;

        public TMP_Text YValue;

        public TMP_Text ZValue;

        public TMP_Text DevicePositionXValue;

        public TMP_Text DevicePositionYValue;

        public TMP_Text DevicePositionZValue;

        public TMP_Text DeviceRotationXValue;

        public TMP_Text DeviceRotationYValue;

        public TMP_Text DeviceRotationZValue;

        public TMP_Text SelectionRayPositionXValue;

        public TMP_Text SelectionRayPositionYValue;

        public TMP_Text SelectionRayPositionZValue;

        public TMP_Text SelectionRayRotationXValue;

        public TMP_Text SelectionRayRotationYValue;

        public TMP_Text SelectionRayRotationZValue;
    }
}
