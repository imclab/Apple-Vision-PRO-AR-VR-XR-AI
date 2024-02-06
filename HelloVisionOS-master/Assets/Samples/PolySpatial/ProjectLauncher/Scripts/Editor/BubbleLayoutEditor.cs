using UnityEditor;
using UnityEngine;

namespace PolySpatial.Samples
{
    [CustomEditor(typeof(BubbleLayoutManager))]
    public class BubbleLayoutEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BubbleLayoutManager manager = (BubbleLayoutManager)target;

            if (GUILayout.Button("Arrange Bubbles"))
            {
                manager.LayoutBubbles();
            }
        }
    }
}
