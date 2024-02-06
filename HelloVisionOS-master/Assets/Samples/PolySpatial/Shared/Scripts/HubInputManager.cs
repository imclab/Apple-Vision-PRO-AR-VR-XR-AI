using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class HubInputManager : MonoBehaviour
    {
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
                if (activeTouches[0].phase == TouchPhase.Began)
                {
                    var buttonObject = primaryTouchData.targetObject;
                    if (buttonObject != null)
                    {
                        if (buttonObject.TryGetComponent(out HubButton button))
                        {
                            button.Press();
                        }
                    }
                }
            }
        }
    }
}
