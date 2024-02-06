using System;
using UnityEngine;

#if UNITY_INCLUDE_ARFOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    Transform m_CameraTransform;

    [SerializeField]
    GameObject m_HeadPoseReticle;

#if UNITY_INCLUDE_ARFOUNDATION
    GameObject m_SpawnedHeadPoseReticle;
    RaycastHit m_HitInfo;

    void Start()
    {
        m_SpawnedHeadPoseReticle = Instantiate(m_HeadPoseReticle, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (Physics.Raycast(new Ray(m_CameraTransform.position, m_CameraTransform.forward), out m_HitInfo))
        {
            if (m_HitInfo.transform.TryGetComponent(out ARPlane plane))
            {
                m_SpawnedHeadPoseReticle.transform.SetPositionAndRotation(m_HitInfo.point, Quaternion.FromToRotation(m_SpawnedHeadPoseReticle.transform.up, m_HitInfo.normal));
            }
        }
    }
#endif
}
