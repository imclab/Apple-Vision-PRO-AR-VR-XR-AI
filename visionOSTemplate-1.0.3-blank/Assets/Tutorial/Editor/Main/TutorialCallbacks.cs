using UnityEngine;
using UnityEditor;
using Unity.Tutorials.Core.Editor;
using UnityEditor.Overlays;

namespace Unity.PolySpatial.Tutorial
{
    internal class TutorialCallbacks : ScriptableObject
    {
        Overlay m_BlocksOverlay;
        Vector2 m_BlocksInitialPosition;

        /// <summary>
        /// Looks for an Editor Window of the given type and closes all of the matches.
        /// </summary>
        /// <param name="windowType"> The type of the EditorWindow to close, written as a string.</param>
        public void CloseEditorWindow(string windowType)
        {
            if (windowType.IsNullOrEmpty()) return;

            var windows = Resources.FindObjectsOfTypeAll(typeof(EditorWindow));
            foreach (var window in windows)
            {
                if (window.GetType().ToString() == windowType)
                {
                    var editorWindow = (EditorWindow) window;
                    editorWindow.Close();
                }
            }
        }

        /// <summary>
        /// Locks the "XR Building Blocks" overlay so that the user cannot change its position or "collapsed" state.
        /// </summary>
        public void LockBuildingBlocksOverlay()
        {
            EditorWindow scene = EditorWindow.GetWindow<SceneView>();
            if (scene.TryGetOverlay("XR Building Blocks", out m_BlocksOverlay))
            {
                m_BlocksInitialPosition = m_BlocksOverlay.floatingPosition;
                m_BlocksOverlay.floatingPositionChanged += OnBuildingBlocksPosChanged;
                m_BlocksOverlay.collapsedChanged += OnBuildingBlocksCollapsedChanged;
            }
        }

        void OnBuildingBlocksCollapsedChanged(bool isCollapsed) => m_BlocksOverlay.collapsed = false;
        void OnBuildingBlocksPosChanged(Vector3 Post) => m_BlocksOverlay.floatingPosition = m_BlocksInitialPosition;

        /// <summary>
        /// Unlocks the "XR Building Blocks" overlay so that the user can resume changing its position or "collapsed" state.
        /// </summary>
        public void UnlockBuildingBlocksOverlay()
        {
            m_BlocksOverlay.floatingPositionChanged -= OnBuildingBlocksPosChanged;
            m_BlocksOverlay.collapsedChanged -= OnBuildingBlocksCollapsedChanged;
        }
    }
}
