using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class CharacterInputManager : MonoBehaviour
    {
        const float k_DeadZoneDistance = 0.04f;
        const float k_MaxDistance = 0.5f;
        const float k_FloorYPosition = -0.2f;
        static readonly int k_MoveSpeed = Animator.StringToHash("Speed");

        [SerializeField]
        Animator m_CharacterAnimator;

        [SerializeField]
        Transform m_CharacterTransform;

        [SerializeField]
        Transform m_TargetTransform;

        [SerializeField]
        InputActionReference m_PrimaryTouch;

        [SerializeField]
        BoxCollider m_PlatformCollider;

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            m_PrimaryTouch.action.Enable();
        }

        void Update()
        {
            var activeTouches = Touch.activeTouches;
            var primaryTouchData = m_PrimaryTouch.action.ReadValue<SpatialPointerState>();

            if (activeTouches.Count > 0)
            {
                var primaryTouchPhase = activeTouches[0].phase;

                if (primaryTouchPhase == TouchPhase.Began || primaryTouchPhase == TouchPhase.Moved)
                {
                    // keep target position aligned with ground
                    var worldPosition = primaryTouchData.interactionPosition;
                    var targetPosition = new Vector3(worldPosition.x, k_FloorYPosition, worldPosition.z);

                    // Make sure the selected target position is within the bounds of the platform
                    if (m_PlatformCollider.bounds.Contains(targetPosition))
                    {
                        m_TargetTransform.position = targetPosition;
                    }
                }
            }

            // Move the character on every update, if the distance from target is within the dead zone,
            // it should just be setting move speed to 0
            MoveCharacter();
        }

        void MoveCharacter()
        {
            var distance = Vector3.Distance(m_CharacterTransform.position, m_TargetTransform.position);

            if (distance >= k_DeadZoneDistance)
            {
                var position = m_TargetTransform.position;
                m_CharacterTransform.position = Vector3.Lerp(m_CharacterTransform.position, position, Time.deltaTime);
                m_CharacterTransform.LookAt(position);

                var normalizedSpeedValue = distance / k_MaxDistance;

                m_CharacterAnimator.SetFloat(k_MoveSpeed, normalizedSpeedValue);
            }
            else
            {
                m_CharacterAnimator.SetFloat(k_MoveSpeed, 0.0f);
            }
        }
    }
}
