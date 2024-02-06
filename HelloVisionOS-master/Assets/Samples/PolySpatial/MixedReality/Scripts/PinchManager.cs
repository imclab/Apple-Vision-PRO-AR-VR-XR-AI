using UnityEngine;

#if UNITY_INCLUDE_XR_HANDS
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
#endif

namespace PolySpatial.Samples
{
    public class PinchManager : MonoBehaviour
    {
        [SerializeField]
        GameObject m_LeftSpawnPrefab;

        [SerializeField]
        GameObject m_RightSpawnPrefab;

#if UNITY_INCLUDE_XR_HANDS
        XRHandSubsystem m_Subsystem;
        XRHandJoint m_LeftThumbTipJoint;
        XRHandJoint m_LeftIndexTipJoint;
        XRHandJoint m_RightThumbTipJoint;
        XRHandJoint m_RightIndexTipJoint;
        Vector3 m_LeftMidPoint;
        Vector3 m_RightMidPoint;

        const float k_PinchThreshold = 0.02f;

        void Update()
        {
            if (TryEnsureInitialized())
            {
                if (CalculatePinch(m_RightIndexTipJoint, m_RightThumbTipJoint, ref m_RightMidPoint))
                {
                    Instantiate(m_RightSpawnPrefab, m_RightMidPoint, Quaternion.identity);
                }

                if (CalculatePinch(m_LeftIndexTipJoint, m_LeftThumbTipJoint, ref m_LeftMidPoint))
                {
                    Instantiate(m_LeftSpawnPrefab, m_LeftMidPoint, Quaternion.identity);
                }
            }
        }

        bool TryEnsureInitialized()
        {
            if (m_Subsystem != null)
                return true;

            m_Subsystem = XRGeneralSettings.Instance?.Manager?.activeLoader?.GetLoadedSubsystem<XRHandSubsystem>();
            if (m_Subsystem == null)
                return false;

            // m_Subsystem.handsUpdated += HandsUpdated;
            return true;
        }

        void HandsUpdated(XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags, XRHandSubsystem.UpdateType updateType)
        {
            if ((updateSuccessFlags & XRHandSubsystem.UpdateSuccessFlags.LeftHandRootPose) !=
                XRHandSubsystem.UpdateSuccessFlags.None)
            {
                m_LeftThumbTipJoint = m_Subsystem.leftHand.GetJoint(XRHandJointID.ThumbTip);
                m_LeftIndexTipJoint = m_Subsystem.leftHand.GetJoint(XRHandJointID.IndexTip);
            }

            if ((updateSuccessFlags & XRHandSubsystem.UpdateSuccessFlags.RightHandRootPose) !=
                XRHandSubsystem.UpdateSuccessFlags.None)
            {
                m_RightThumbTipJoint = m_Subsystem.rightHand.GetJoint(XRHandJointID.ThumbTip);
                m_RightIndexTipJoint = m_Subsystem.rightHand.GetJoint(XRHandJointID.IndexTip);
            }
        }

        bool CalculatePinch(XRHandJoint indexJoint, XRHandJoint thumbJoint, ref Vector3 midPoint)
        {
            var indexPOS = Vector3.zero;
            var thumbPOS = Vector3.zero;

            if (indexJoint.TryGetPose(out Pose indexPose))
            {
                indexPOS = indexPose.position;
            }

            if (thumbJoint.TryGetPose(out Pose thumbPose))
            {
                thumbPOS = thumbPose.position;
            }

            if (Vector3.Distance(indexPOS, thumbPOS) <= k_PinchThreshold)
            {
                var pinchMidPoint = (indexPOS + thumbPOS) / 2;
                midPoint = pinchMidPoint;
                return true;
            }

            return false;
        }
#endif
    }
}

