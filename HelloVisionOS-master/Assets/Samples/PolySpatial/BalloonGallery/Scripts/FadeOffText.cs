using TMPro;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class FadeOffText : MonoBehaviour
    {
        Color m_StartingColor;
        TMP_Text m_Text;
        float m_Time;

        const float k_FadeTime = 1.0f;
        const float k_FadeDelay = 0.0f;

        void Start()
        {
            m_Text = GetComponent<TMP_Text>();
            m_StartingColor = m_Text.color;
        }

        void Update()
        {
            m_Time += Time.deltaTime;

            if (m_Time >= k_FadeDelay)
            {
                var fadedValue = 1 - (m_Time / k_FadeTime);
                var fadedColor = new Color(m_StartingColor.r, m_StartingColor.g, m_StartingColor.b, fadedValue);
                m_Text.color = fadedColor;
            }
        }
    }
}
