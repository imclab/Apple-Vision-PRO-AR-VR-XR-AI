using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolySpatial.Samples
{
    public class LoadLevelButton : HubButton
    {
        [SerializeField]
        string m_LevelName;

        [SerializeField]
        LevelData.LevelTypes m_LevelType;

        public LevelData.LevelTypes LevelType => m_LevelType;

        public override void Press()
        {
            base.Press();
            SceneManager.LoadScene(m_LevelName);
        }
    }
}
