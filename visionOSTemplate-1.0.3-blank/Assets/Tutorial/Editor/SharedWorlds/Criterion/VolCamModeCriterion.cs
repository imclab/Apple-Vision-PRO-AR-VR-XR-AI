using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.PolySpatial.Tutorial
{
    class VolCamModeCriterion : Criterion
    {
        VolumeCamera m_VolumeCamera;

        public override void StartTesting()
        {
            m_VolumeCamera = FindObjectOfType<VolumeCamera>();

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
            if (m_VolumeCamera == null)
                return false;

            if (m_VolumeCamera.WindowConfiguration == null)
                return false;

            return m_VolumeCamera.WindowConfiguration.Mode == VolumeCamera.PolySpatialVolumeCameraMode.Bounded;
        }

        public override bool AutoComplete()
        {
            // Configuration's mode has no setter and no constructor, so cannot autocomplete this step.
            return true;
        }
    }
}
