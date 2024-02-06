using Unity.PolySpatial;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.PolySpatial.Tutorial
{
    class VolCamDimensionCriterion : Criterion
    {
        VolumeCamera m_VolCam;
        Vector3 m_InitialDimension;

        public override void StartTesting()
        {
            m_VolCam = FindObjectOfType<VolumeCamera>();
            m_InitialDimension = m_VolCam == null? Vector3.one : m_VolCam.Dimensions;

            base.StartTesting();
            UpdateCompletion();
            EditorApplication.update += UpdateCompletion;
        }

        public override void StopTesting()
        {
            base.StopTesting();
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            if (m_VolCam == null)
                return true;
            return !m_VolCam.Dimensions.Equals(m_InitialDimension);
        }

        public override bool AutoComplete()
        {
            if (m_VolCam == null)
                return true;
            m_VolCam.Dimensions = m_InitialDimension * 2;

            return true;
        }
    }
}
