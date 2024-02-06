using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolySpatial.Samples
{
    public class SpatialUIEditorTesting : MonoBehaviour
    {
        SpatialUI[] m_SpatialUIElements;

        [SerializeField]
        SpatialUIButton m_Button;

        [SerializeField]
        SpatialUIButton m_Button2;

        [SerializeField]
        SpatialUI m_ToggleButton;

        [SerializeField]
        SpatialUIDropdown m_Dropdown;

        void Start()
        {
            m_SpatialUIElements = FindObjectsOfType<SpatialUI>();
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                m_Dropdown.Press(Vector3.zero);

                foreach (var spatialUIElement in m_SpatialUIElements)
                {
                    //spatialUIElement.Press();
                }
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                m_Button.Press(Vector3.zero);
            }

            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                m_Button2.Press(Vector3.zero);
            }

            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                m_ToggleButton.Press(Vector3.zero);
            }
        }
    }
}
