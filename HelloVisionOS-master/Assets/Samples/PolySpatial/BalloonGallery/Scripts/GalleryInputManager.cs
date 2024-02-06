using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class GalleryInputManager : MonoBehaviour
    {
        [SerializeField]
        Transform m_InputAxisTransform;

        void OnEnable()
        {
            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        void Update()
        {
            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                if (activeTouches[0].phase == TouchPhase.Began)
                {
                    // allow balloons to be popped with a poke or indirect pinch
                    if (primaryTouchData.Kind == SpatialPointerKind.IndirectPinch || primaryTouchData.Kind == SpatialPointerKind.Touch)
                    {
                        var balloonObject = primaryTouchData.targetObject;
                        if (balloonObject != null)
                        {
                            if (balloonObject.TryGetComponent(out BalloonBehavior balloon))
                            {
                                balloon.Pop();
                            }
                        }
                    }

                    // update input gizmo
                    m_InputAxisTransform.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.deviceRotation);
                }

                // visualize input gizmo while input is maintained
                if (activeTouches[0].phase == TouchPhase.Moved)
                {
                    m_InputAxisTransform.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.deviceRotation);
                }
            }
        }
    }
}
