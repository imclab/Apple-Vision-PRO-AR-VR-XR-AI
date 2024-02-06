using TMPro;
using UnityEngine;

#if UNITY_INCLUDE_ARFOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

namespace PolySpatial.Samples
{
#if UNITY_INCLUDE_ARFOUNDATION
    [RequireComponent(typeof(ARPlane))]
#endif
    public class PlaneDataUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text m_AlignmentText;

        [SerializeField]
        TMP_Text m_ClassificationText;

#if UNITY_INCLUDE_ARFOUNDATION
        ARPlane m_Plane;

        void OnEnable()
        {
            m_Plane = GetComponent<ARPlane>();
            m_Plane.boundaryChanged += OnBoundaryChanged;
        }

        void OnDisable()
        {
            m_Plane.boundaryChanged -= OnBoundaryChanged;
        }

        void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs eventArgs)
        {
            m_ClassificationText.text = m_Plane.classification.ToString();
            m_AlignmentText.text = m_Plane.alignment.ToString();

            transform.position = m_Plane.center;
        }
#endif
    }
}
