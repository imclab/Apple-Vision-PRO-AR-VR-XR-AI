using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Unity.PolySpatial.Tutorial
{
    class AddParticleSystemCriterion : Criterion
    {
        const string k_URPParticleShaderPath = "Universal Render Pipeline/Particles/Unlit";
        const string k_ParticleTextureName = "Default-Particle";
        const string k_SurfaceKeyword = "_Surface";
        const string k_AlphaTestKeyword = "_ALPHATEST_ON";

        ParticleSystem.MainModule m_ParticleSystemModule;

        public override void StartTesting()
        {
            GameObject newObject = new GameObject("IET Particle System");
            newObject.transform.position = Vector3.zero;
            var particleSystem = newObject.AddComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                m_ParticleSystemModule = particleSystem.main;
                m_ParticleSystemModule.startRotation3D = true;
                var psr = newObject.GetComponent<ParticleSystemRenderer>();
                psr.material = createParticleMaterial();
            }

            Selection.activeObject = newObject;
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
            return m_ParticleSystemModule.startRotation3D == true;
        }

        public override bool AutoComplete()
        {
            m_ParticleSystemModule.startRotation3D = true;
            return true;
        }

        Material createParticleMaterial()
        {
            var particleShader = Shader.Find(k_URPParticleShaderPath);
            var particleMat = new Material(particleShader);
            Texture particleTexture = null;

            foreach (var pText in Resources.FindObjectsOfTypeAll<Texture>())
                if (pText.name == k_ParticleTextureName)
                    particleTexture = pText;

            particleMat.mainTexture = particleTexture;
            particleMat.SetFloat(k_SurfaceKeyword, 1);
            particleMat.EnableKeyword(k_AlphaTestKeyword);

            return particleMat;
        }
    }
}
