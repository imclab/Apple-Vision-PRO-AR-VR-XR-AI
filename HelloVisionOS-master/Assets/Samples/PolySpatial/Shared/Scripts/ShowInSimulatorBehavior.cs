using System;
using UnityEngine;

namespace PolySpatial.Samples
{
    internal class ShowInSimulatorBehavior : MonoBehaviour
    {
        [SerializeField]
        GameObject m_ObjectToShow;

        void Start()
        {
            var simRoot = Environment.GetEnvironmentVariable("SIMULATOR_ROOT") != null;
            m_ObjectToShow.SetActive(simRoot);
        }
    }
}
