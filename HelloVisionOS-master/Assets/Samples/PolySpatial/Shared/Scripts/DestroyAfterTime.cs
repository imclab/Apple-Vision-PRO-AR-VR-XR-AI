using UnityEngine;

namespace PolySpatial.Samples
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [SerializeField]
        float m_LifeTime = 0.5f;

        void Start()
        {
            Invoke("DestroyThisObject", m_LifeTime);
        }

        void DestroyThisObject()
        {
            Destroy(this.gameObject);
        }
    }
}
