using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.PolySpatial.Tutorial
{
    class ClickedVolCamCriterion : Criterion
    {
        readonly List<VisualElement> m_SceneViewsRoots = new List<VisualElement>();
        bool m_ClickedVolCam;
        const string k_DropdownName = "BuildingBlockSectionDropdown Setup";
        const string k_FoldoutName = "BuildingBlockSectionFoldout Setup";
        const string k_ButtonName = "BuildingBlockButton Volume Camera";
        const string k_ToggleName = "Toggle Setup";

        public override void StartTesting()
        {
            m_SceneViewsRoots.Clear();
            foreach (var sceneView in Resources.FindObjectsOfTypeAll<SceneView>())
            {
                m_SceneViewsRoots.Add(sceneView.rootVisualElement);
            }

            m_ClickedVolCam = false;

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
            foreach (var visualElement in m_SceneViewsRoots)
            {
                var currentElement = visualElement?.focusController?.focusedElement;

                if (currentElement != null)
                {
                    var currentName = currentElement.ToString();
                    if (currentName.Contains(k_DropdownName)  || currentName.Contains(k_FoldoutName) || currentName.Contains(k_ButtonName) || currentName.Contains(k_ToggleName))
                    {
                        m_ClickedVolCam = true;
                    }
                }
            }

            return m_ClickedVolCam;
        }

        public override bool AutoComplete()
        {
            m_ClickedVolCam = true;
            return m_ClickedVolCam;
        }
    }
}
