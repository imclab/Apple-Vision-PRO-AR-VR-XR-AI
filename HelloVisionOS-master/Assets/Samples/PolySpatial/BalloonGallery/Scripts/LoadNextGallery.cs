using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PolySpatial.Samples
{
    public class LoadNextGallery : MonoBehaviour
    {
        [SerializeField]
        string m_SceneName;

        void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                LoadGallery();
            }
        }

        public void LoadGallery()
        {
            SceneManager.LoadScene(m_SceneName);
        }
    }
}
