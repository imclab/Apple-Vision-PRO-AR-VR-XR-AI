using UnityEngine;

namespace PolySpatial.Samples
{
    [RequireComponent(typeof(Rigidbody))]
    public class PieceSelectionBehavior : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer m_MeshRenderer;

        [SerializeField]
        Material m_DefaultMat;

        [SerializeField]
        Material m_SelectedMat;

        Rigidbody m_Rigidbody;
        GameObject m_TempParent;

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();

            m_TempParent = new GameObject("tempParent_" + gameObject.name);
            m_TempParent.transform.position = transform.position;
        }

        public void Select(bool selected, Vector3 interactionPosition)
        {
            m_TempParent.transform.position = interactionPosition;
            transform.SetParent(selected ? m_TempParent.transform : null);
            m_MeshRenderer.material = selected ? m_SelectedMat : m_DefaultMat;
            m_Rigidbody.isKinematic = selected;
        }
    }
}
