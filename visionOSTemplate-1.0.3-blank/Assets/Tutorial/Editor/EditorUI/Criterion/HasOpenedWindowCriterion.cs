using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.PolySpatial.Tutorial
{
    class HasOpenedWindowCriterion : Criterion
    {
        [SerializedTypeFilter(typeof(EditorWindow), false)]
        [SerializeField]
        SerializedType m_EditorWindowType = new SerializedType(null);

        EditorWindow m_WindowInstance;
        bool m_HasOpenedWindow;
        public override void StartTesting()
        {
            m_HasOpenedWindow = false;
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
            if (m_EditorWindowType.Type != null)
            {
                if (!m_WindowInstance)
                {
                    var windows = Resources.FindObjectsOfTypeAll(m_EditorWindowType.Type);

                    foreach (var w in windows)
                    {
                        if (w.GetType() == m_EditorWindowType.Type)
                        {
                            m_WindowInstance = (EditorWindow)w;

                            m_WindowInstance.Focus();
                            m_HasOpenedWindow = true;
                        }
                    }
                }
                else if (m_WindowInstance.GetType() != m_EditorWindowType.Type)
                {
                    m_WindowInstance = null;
                }
            }

            return m_HasOpenedWindow;
        }

        public override bool AutoComplete()
        {
            if (m_EditorWindowType.Type != null)
            {
                EditorWindow.GetWindow(m_EditorWindowType.Type);
                m_HasOpenedWindow = true;
            }

            return m_HasOpenedWindow;
        }
    }
}
