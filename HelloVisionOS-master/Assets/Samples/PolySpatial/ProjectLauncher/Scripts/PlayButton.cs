using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolySpatial.Samples
{
    public class PlayButton : HubButton
    {
        [SerializeField]
        LevelBubbleManager m_LevelBubbleManager;

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Press();
            }
        }

        public override void Press()
        {
            m_LevelBubbleManager.LoadSelectedLevel();
        }
    }
}
