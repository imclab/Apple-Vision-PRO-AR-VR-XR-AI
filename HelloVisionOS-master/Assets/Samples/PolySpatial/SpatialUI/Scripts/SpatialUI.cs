using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolySpatial.Samples
{
    public class SpatialUI : MonoBehaviour
    {

        public Color SelectedColor = new Color(.1254f, .5882f, .9529f);
        public Color UnselectedColor = new Color(.1764f, .1764f, .1764f);

        // TODO Hover component?

        public virtual void Press(Vector3 position)
        {
            // TODO audio?
        }
    }
}
