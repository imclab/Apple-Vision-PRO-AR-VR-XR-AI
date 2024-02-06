using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolySpatial.Samples
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSFX : MonoBehaviour
    {
        [SerializeField]
        List<AudioClip> m_AudioClips;

        AudioSource m_Source;

        void Awake()
        {
            m_Source = GetComponent<AudioSource>();

            var randomIndex = Random.Range(0, m_AudioClips.Count);
            m_Source.clip = m_AudioClips[randomIndex];
            m_Source.Play();
        }
    }
}
