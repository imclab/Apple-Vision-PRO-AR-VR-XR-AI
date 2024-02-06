using System.Collections.Generic;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class BubbleLayoutManager : MonoBehaviour
    {
        [SerializeField]
        List<Transform> m_LevelBubbles;

        [SerializeField]
        float m_CarouselRadius = 0.5f;

        [SerializeField]
        Transform m_BubbleRoot;

        public float BubbleSpacing => 360.0f / m_LevelBubbles.Count;

        float m_StatingYOffset;

        void Start()
        {
            m_StatingYOffset = m_BubbleRoot.transform.position.y;
        }

        public void LayoutBubbles()
        {
            var spacing = 360.0f / m_LevelBubbles.Count;

            for (int i = 0; i < m_LevelBubbles.Count; i++)
            {
                var radians = Mathf.Deg2Rad * (spacing * i);
                var x = m_CarouselRadius * Mathf.Sin(radians);
                var z = m_CarouselRadius * Mathf.Cos(radians);
                m_LevelBubbles[i].position = new Vector3(x, m_StatingYOffset, z);
                m_LevelBubbles[i].parent = m_BubbleRoot;
            }
        }
    }
}
