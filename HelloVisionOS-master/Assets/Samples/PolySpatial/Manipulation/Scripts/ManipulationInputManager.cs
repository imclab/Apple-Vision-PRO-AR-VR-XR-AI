using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    /// <summary>
    /// Current you can only select one object at a time and only supports a primary [0] touch
    /// </summary>
    public class ManipulationInputManager : MonoBehaviour
    {
        PieceSelectionBehavior m_CurrentSelection;

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
                
                if (primaryTouchData.Kind == SpatialPointerKind.DirectPinch || primaryTouchData.Kind == SpatialPointerKind.IndirectPinch)
                {
                    var pieceObject = primaryTouchData.targetObject;
                    if (pieceObject != null)
                    {
                        if (pieceObject.TryGetComponent(out PieceSelectionBehavior piece))
                        {
                            m_CurrentSelection = piece;
                            m_CurrentSelection.Select(true, primaryTouchData.interactionPosition);
                        }
                    }

                    if (activeTouches[0].phase == TouchPhase.Moved)
                    {
                        if (m_CurrentSelection != null)
                        {
                            m_CurrentSelection.transform.parent.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.deviceRotation);
                        }
                    }

                    if (activeTouches[0].phase == TouchPhase.Ended || activeTouches[0].phase == TouchPhase.Canceled)
                    {
                        if (m_CurrentSelection != null)
                        {
                            m_CurrentSelection.Select(false, primaryTouchData.interactionPosition);
                            m_CurrentSelection = null;
                        }
                    }
                }
                else
                {
                    if (m_CurrentSelection != null)
                    {
                        m_CurrentSelection.Select(false, primaryTouchData.interactionPosition);
                        m_CurrentSelection = null;
                    }
                }
            }
        }
    }
}
