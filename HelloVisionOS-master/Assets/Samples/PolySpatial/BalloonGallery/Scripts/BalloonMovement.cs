using UnityEngine;

namespace PolySpatial.Samples
{
    public class BalloonMovement : MonoBehaviour
    {
        [SerializeField]
        float m_MovementMultiplier = 1.0f;

        [SerializeField]
        bool m_Activate = true;

        public bool Activate
        {
            set => m_Activate = value;
        }

        float m_StartingX;
        float m_StartingZ;
        float m_StartingDelay;
        float m_DelayTime;
        float m_StartTime;
        bool m_NotSet;
        bool m_BalloonMoving;

        const float k_HeightMod = 0.15f;
        const float k_MinStartDelay = 1.1f;
        const float k_MaxStartDelay = 5.0f;
        const float k_StartingOffset = 0.02f;

        void Start()
        {
            m_StartingX = transform.position.x;
            m_StartingZ = transform.position.z;

            m_StartingDelay = Random.Range(k_MinStartDelay, k_MaxStartDelay);
        }

        void Update()
        {
            if (m_Activate)
            {
                m_DelayTime += Time.deltaTime;

                if (m_DelayTime >= m_StartingDelay && !m_BalloonMoving)
                {
                    m_StartTime = Time.time;
                    m_BalloonMoving = true;
                }
            }

            if (m_BalloonMoving)
            {
                var sinValue = Time.time - m_StartTime - 1.0f;
                var yHeight = Mathf.Sin(sinValue * m_MovementMultiplier) * k_HeightMod;

                transform.position = new Vector3(m_StartingX, yHeight + k_HeightMod - k_StartingOffset, m_StartingZ);
            }
        }
    }
}
