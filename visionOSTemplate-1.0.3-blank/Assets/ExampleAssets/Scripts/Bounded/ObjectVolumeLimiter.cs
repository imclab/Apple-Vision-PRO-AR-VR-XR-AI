using UnityEngine;

namespace PolySpatial.Template
{
    public class ObjectVolumeLimiter : MonoBehaviour
    {
        [SerializeField]
        float m_BoxSize = 1.0f;

        [SerializeField]
        float m_BufferSize = 0.1f;

        Transform m_Transform;
        float m_MinPosition;
        float m_MaxPosition;
        float m_ClampedX;
        float m_ClampedY;
        float m_ClampedZ;

        void Start()
        {
            m_Transform = transform;
            m_MinPosition = -(m_BoxSize / 2) + m_BufferSize;
            m_MaxPosition = (m_BoxSize / 2) - m_BufferSize;
        }

        void Update()
        {
            var position = m_Transform.position;
            m_ClampedX = Mathf.Clamp(position.x, m_MinPosition, m_MaxPosition);
            m_ClampedY = Mathf.Clamp(position.y, m_MinPosition, m_MaxPosition);
            m_ClampedZ = Mathf.Clamp(position.z, m_MinPosition, m_MaxPosition);
            m_Transform.position = new Vector3(m_ClampedX, m_ClampedY, m_ClampedZ);
        }
    }
}
