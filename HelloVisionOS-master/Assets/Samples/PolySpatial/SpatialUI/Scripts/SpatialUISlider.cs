using UnityEngine;
using UnityEngine.UI;

namespace PolySpatial.Samples
{
    public class SpatialUISlider : SpatialUI
    {
        [SerializeField]
        Image m_FillImage;

        float m_BoxColliderSizeX;

        void Start()
        {
            m_BoxColliderSizeX = GetComponent<BoxCollider>().size.x;
        }

        public override void Press(Vector3 position)
        {
            base.Press(position);
            var localPosition = transform.InverseTransformPoint(position);
            var percentage = (localPosition.x + m_BoxColliderSizeX / 2) / m_BoxColliderSizeX;
            m_FillImage.fillAmount = Mathf.Clamp(1.0f - percentage, 0.0f, 1.0f);
        }
    }
}
