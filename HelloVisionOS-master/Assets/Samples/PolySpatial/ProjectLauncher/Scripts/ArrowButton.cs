using UnityEngine;

namespace PolySpatial.Samples
{
    public class ArrowButton : HubButton
    {
        [SerializeField] LevelBubbleManager m_LevelBubbleManager;

        [SerializeField] bool m_Left;

        public override void Press()
        {
            base.Press();
            m_LevelBubbleManager.ArrowButtonPressed(m_Left);
        }
    }
}
