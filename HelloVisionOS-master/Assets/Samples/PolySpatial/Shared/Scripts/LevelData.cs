using UnityEngine;

namespace PolySpatial.Samples
{
    public class LevelData : MonoBehaviour
    {
        public enum LevelTypes
        {
            CharacterNavigation,
            TargetedInput,
            ImageTracking,
            ObjectManipulation,
            SpatialUI,
            InputDebug,
            MixedReality
        }

        const string CharacterNavigationTitle = "Character Navigation";
        const string TargetedInputTitle = "Targeted Input";
        const string ImageTrackingTitle = "Image Tracking";
        const string ObjectManipulationTitle = "Object Manipulation";
        const string SpatialUITitle = "Spatial UI";
        const string InputDebugTitle = "Input Debug";
        const string MixedRealityTitle = "Mixed Reality";

        const string CharacterNavigationDescription = "Scene demonstrating a character navigating around with a dynamic camera in a bounded volume.";

        const string TargetedInputDescription =
            "Scene demonstrating targeted input and particle systems through the use of a simple balloon gallery mini game.";

        const string ImageTrackingDescription = "Scene demonstrating spawning content relative to unique tracked images in an unbounded space.";
        const string ObjectManipulationDescription = "Scene demonstrating manipulating 3D assets within a bounded volume.";
        const string SpatialUIDescription = "Scene demonstrating interactions with common spatial UI elements within a bounded volume.";
        const string InputDebugDescription = "Scene displaying input data generated from direct and indirect touches within a bounded volume.";
        const string MixedRealityDescription = "Scene demonstrating an unbounded app that displays AR Plane data and uses a custom ARKit hand gesture.";

        public string GetLevelTitle(LevelTypes levelType)
        {
            switch (levelType)
            {
                case LevelTypes.CharacterNavigation:
                    return CharacterNavigationTitle;
                case LevelTypes.TargetedInput:
                    return TargetedInputTitle;
                case LevelTypes.ImageTracking:
                    return ImageTrackingTitle;
                case LevelTypes.ObjectManipulation:
                    return ObjectManipulationTitle;
                case LevelTypes.SpatialUI:
                    return SpatialUITitle;
                case LevelTypes.InputDebug:
                    return InputDebugTitle;
                case LevelTypes.MixedReality:
                    return MixedRealityTitle;
                default:
                    return "";
            }
        }

        public string GetLevelDescription(LevelTypes levelType)
        {
            switch (levelType)
            {
                case LevelTypes.CharacterNavigation:
                    return CharacterNavigationDescription;
                case LevelTypes.TargetedInput:
                    return TargetedInputDescription;
                case LevelTypes.ImageTracking:
                    return ImageTrackingDescription;
                case LevelTypes.ObjectManipulation:
                    return ObjectManipulationDescription;
                case LevelTypes.SpatialUI:
                    return SpatialUIDescription;
                case LevelTypes.InputDebug:
                    return InputDebugDescription;
                case LevelTypes.MixedReality:
                    return MixedRealityDescription;
                default:
                    return "";
            }
        }
    }
}
