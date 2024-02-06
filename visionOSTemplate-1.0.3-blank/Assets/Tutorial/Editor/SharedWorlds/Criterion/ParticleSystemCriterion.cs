using Unity.Tutorials.Core.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Unity.PolySpatial.Tutorial
{
    class ParticleSystemCriterion : Criterion
    {
        readonly List<VisualElement> m_SceneViewsRoots = new List<VisualElement>();
        ParticleSystem.MainModule m_ParticleSystemModule;
        GameObject m_ParticleSystem;

        public override void StartTesting()
        {
            var particleSystem = FindObjectOfType<ParticleSystem>();
            if (particleSystem != null)
            {
                m_ParticleSystem = particleSystem.gameObject;
                m_ParticleSystemModule = particleSystem.main;
            }

            base.StartTesting();
            UpdateCompletion();
            EditorApplication.update += UpdateCompletion;
        }

        public override void StopTesting()
        {
            base.StopTesting();
            DestroyImmediate(m_ParticleSystem);
            EditorApplication.update -= UpdateCompletion;
        }

        protected override bool EvaluateCompletion()
        {
            return m_ParticleSystemModule.startRotation3D == false;
        }

        public override bool AutoComplete()
        {
            m_ParticleSystemModule.startRotation3D = false;
            return true;
        }
    }
}
