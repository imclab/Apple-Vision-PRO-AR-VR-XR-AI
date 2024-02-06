using System.Linq;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.PolySpatial.Tutorial
{
    class BBOverlayIsOpenCriterion : Criterion
    {
        SceneView[] m_SceneViews;
        const string k_OverlayId = "XR Building Blocks";

        public override void StartTesting()
        {
            m_SceneViews = Resources.FindObjectsOfTypeAll<SceneView>();

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
            if (m_SceneViews.Length == 0 || m_SceneViews.All(s => s == null))
                m_SceneViews = Resources.FindObjectsOfTypeAll<SceneView>();

            foreach (var sceneView in m_SceneViews)
            {
                if (sceneView.TryGetOverlay(k_OverlayId, out var match))
                {
                    if (match.displayed)
                        return true;
                }
            }

            return false;
        }

        public override bool AutoComplete()
        {
            if (m_SceneViews.Length >= 1 && m_SceneViews[0].TryGetOverlay(k_OverlayId, out var match))
            {
                match.displayed = true;
                return true;
            }

            return false;
        }
    }
}
