using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Template
{
    public class BoundedInputManager : MonoBehaviour
    {
        [SerializeField]
        bool m_UsePhysics = true;

        TouchPhase m_LastTouchPhase;
        BoundedObjectBehavior m_SelectedObject;

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void Update()
        {
            var activeTouches = Touch.activeTouches;

            if (activeTouches.Count > 0)
            {
                var primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
                var touchPhase = activeTouches[0].phase;


                if (touchPhase == TouchPhase.Began)
                {
                    if (primaryTouchData.targetObject != null)
                    {
                        if(primaryTouchData.targetObject.TryGetComponent(out BoundedObjectBehavior boundedObject))
                        {
                            m_SelectedObject = boundedObject;
                            m_SelectedObject.Select(true);
                        }
                    }
                }

                if (touchPhase == TouchPhase.Moved)
                {
                    if (m_SelectedObject != null)
                    {
                        // use physics and move object for non direct pinch (interaction kind 1)
                        if (m_UsePhysics && primaryTouchData.Kind == SpatialPointerKind.IndirectPinch)
                        {
                            m_SelectedObject.MoveWithPhysics(primaryTouchData.interactionPosition);
                        }
                        else
                        {
                            m_SelectedObject.MoveDirectly(primaryTouchData);
                        }
                    }
                }

                if (touchPhase == TouchPhase.Ended || touchPhase == TouchPhase.Canceled)
                {
                    if (m_SelectedObject != null)
                    {
                        m_SelectedObject.Select(false);
                        m_SelectedObject = null;
                    }
                }
            }
        }
    }
}
