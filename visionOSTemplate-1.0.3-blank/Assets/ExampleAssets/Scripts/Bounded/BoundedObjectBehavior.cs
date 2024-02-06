using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace PolySpatial.Template
{
    public class BoundedObjectBehavior : MonoBehaviour
    {
        const float k_ToVel = 7.5f;
        const float k_MaxVel = 15.0f;
        const float k_MaxForce = 40.0f;
        const float k_Gain = 5f;
        const float k_StartingTorque = 0.15f;

        [SerializeField]
        float m_LerpReturnTime = 1.0f;

        [SerializeField]
        float m_Delay = 3.0f;

        [SerializeField]
        Material m_DefaultMaterial;

        [SerializeField]
        Material m_SelectedMaterial;

        Vector3 m_StartingPosition;
        Vector3 m_StartingLerpPosition;
        Transform m_Transform;
        Rigidbody m_Rigidbody;
        MeshRenderer m_MeshRenderer;
        bool m_Selected;
        bool m_Return;
        float m_CurrentTime;
        float m_DelayTime;

        void Start()
        {
            m_Transform = transform;
            m_StartingPosition = m_Transform.position;
            m_MeshRenderer = GetComponent<MeshRenderer>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.AddRelativeTorque(k_StartingTorque, k_StartingTorque, k_StartingTorque);
        }

        void Update()
        {
            // add delay before lerping
            m_DelayTime -= Time.deltaTime;

            if (m_DelayTime <= 0.0f && !m_Return)
            {
                m_StartingLerpPosition = m_Transform.position;
                m_CurrentTime = 0.0f;
                m_Return = true;
            }

            // Lerp to starting position
            if (!m_Selected && m_Return)
            {
                m_CurrentTime += Time.deltaTime;
                if (m_CurrentTime > m_LerpReturnTime)
                {
                    m_CurrentTime = m_LerpReturnTime;
                }

                var time = m_CurrentTime / m_LerpReturnTime;
                // exponential ease out
                time = 1.0f - Mathf.Pow(2.0f, -10.0f * time);

                m_Transform.position = Vector3.Lerp(m_StartingLerpPosition, m_StartingPosition, time);
            }
        }

        public void Select(bool selected)
        {
            m_Return = false;
            m_Selected = selected;
            m_Rigidbody.velocity = Vector3.zero;

            m_MeshRenderer.material = selected ? m_SelectedMaterial : m_DefaultMaterial;

            if (!selected)
            {
                m_DelayTime = m_Delay;
            }
        }

        public void MoveWithPhysics(Vector3 worldPosition)
        {
            var distance = worldPosition - m_Transform.position;
            var targetVelocity = Vector3.ClampMagnitude(k_ToVel * distance, k_MaxVel);
            var error = targetVelocity - m_Rigidbody.velocity;
            var force = Vector3.ClampMagnitude(k_Gain * error, k_MaxForce);
            m_Rigidbody.AddForce(force);
        }

        public void MoveDirectly(SpatialPointerState worldTouch)
        {
            m_Transform.SetPositionAndRotation(worldTouch.interactionPosition, worldTouch.inputDeviceRotation);
        }
    }
}
