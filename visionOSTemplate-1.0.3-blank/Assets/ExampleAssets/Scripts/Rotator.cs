using UnityEngine;

namespace PolySpatial.Template
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        float m_RotationX;

        [SerializeField]
        float m_RotationY;

        [SerializeField]
        float m_RotationZ;

        void Update()
        {
            transform.Rotate(new Vector3(m_RotationX, m_RotationY, m_RotationZ) * Time.deltaTime);
        }
    }
}
