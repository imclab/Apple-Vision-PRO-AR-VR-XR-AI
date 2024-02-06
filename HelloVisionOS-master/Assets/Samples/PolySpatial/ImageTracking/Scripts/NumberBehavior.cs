using System;
using UnityEngine;
using UnityEngine.Animations;

namespace PolySpatial.Samples
{
    public class NumberBehavior : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Number Object which is controlled by this behavior")]
        GameObject m_NumberObject;

        public GameObject numberObject
        {
            get => m_NumberObject;
            set => m_NumberObject = value;
        }

        void Awake()
        {
            var constraint = GetComponentInChildren<AimConstraint>();
            if (constraint == null)
            {
                Debug.LogError($"NumberBehavior {name} couldn't find AimConstraint", this);
                return;
            }

            var source = new ConstraintSource();
            source.sourceTransform = Camera.main.transform;
            source.weight = 1.0f;
            constraint.AddSource(source);
        }
    }
}
