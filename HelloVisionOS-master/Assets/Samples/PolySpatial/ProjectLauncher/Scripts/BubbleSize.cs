using System;
using UnityEngine;

namespace PolySpatial.Samples
{
    public class BubbleSize : MonoBehaviour
    {
        Vector3 m_LocalScale;

        [SerializeField]
        float m_ScaleSpeed = 1.0f;

        public enum BubbleSizeEnum
        {
            ExtraSmall,
            Small,
            Medium,
            Large
        }

        float m_StartTime;
        public Vector3 m_TargetScale;
        Vector3 m_PreviousScale;
        Vector3 m_ExtraSmallScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 m_SmallScale = new Vector3(0.2f, 0.2f, 0.2f);
        Vector3 m_MediumScale = new Vector3(0.3f, 0.3f, 0.3f);
        Vector3 m_LargeScale = new Vector3(0.4f, 0.4f, 0.4f);

        void Awake()
        {
            m_LocalScale = transform.localScale;
            m_PreviousScale = m_TargetScale = m_LocalScale;
        }

        void Update()
        {
            var scalePercent = (Time.time - m_StartTime) * m_ScaleSpeed;
            var scaleDiff = Mathf.Abs(m_TargetScale.x - m_PreviousScale.x);
            if(scaleDiff == 0)
            {
                return;
            }

            var scaleFraction = scalePercent / scaleDiff;
            transform.localScale = Vector3.Lerp(m_PreviousScale, m_TargetScale, scaleFraction);
        }

        public void SetScale(BubbleSizeEnum size)
        {
            Vector3 targetScale = Vector3.zero;
            switch (size)
            {
                case BubbleSizeEnum.ExtraSmall:
                    targetScale = m_ExtraSmallScale;
                    break;
                case BubbleSizeEnum.Small:
                    targetScale = m_SmallScale;
                    break;
                case BubbleSizeEnum.Medium:
                    targetScale = m_MediumScale;
                    break;
                case BubbleSizeEnum.Large:
                    targetScale = m_LargeScale;
                    break;
            }
            m_PreviousScale = transform.localScale;
            m_TargetScale = targetScale;
            m_StartTime = Time.time;
        }
    }
}
