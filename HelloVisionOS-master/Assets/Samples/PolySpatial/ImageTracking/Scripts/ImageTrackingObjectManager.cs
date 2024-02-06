using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_INCLUDE_ARFOUNDATION
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#else
using ARTrackedImageManager = UnityEngine.Object;
using XRReferenceImageLibrary = UnityEngine.Object;
#endif

namespace PolySpatial.Samples
{
    public class ImageTrackingObjectManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Image manager on the AR Session Origin")]
        ARTrackedImageManager m_ImageManager;

        [SerializeField]
        [Tooltip("Reference Image Library")]
        XRReferenceImageLibrary m_ImageLibrary;

        [SerializeField]
        [Tooltip("Prefabs to spawn")]
        GameObject[] m_PrefabsToSpawn;

        readonly Dictionary<Guid, GameObject> m_PrefabsToSpawnDictionary = new();
        readonly Dictionary<Guid, GameObject> m_SpawnedPrefabs = new();
        readonly Dictionary<Guid, NumberBehavior> m_SpawnedNumbers = new();

        public Dictionary<Guid, GameObject> spawnedPrefabs => m_SpawnedPrefabs;

#if UNITY_INCLUDE_ARFOUNDATION
        void Awake()
        {
            var count = m_PrefabsToSpawn.Length;
            var imageCount = m_ImageLibrary.count;
            if (count > imageCount)
                Debug.LogWarning($"Number of prefabs ({count}) exceeds the number of images in the reference library ({imageCount})");

            count = Math.Min(count, imageCount);
            for (var i = 0; i < count; i++)
            {
                var guid = m_ImageLibrary[i].guid;
                m_PrefabsToSpawnDictionary[guid] = m_PrefabsToSpawn[i];
            }
        }

        void OnEnable()
        {
            m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
        }

        void OnDisable()
        {
            m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
        }

        void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
        {
            // added, spawn prefab
            foreach (var image in obj.added)
            {
                var guid = image.referenceImage.guid;
                if (m_PrefabsToSpawnDictionary.TryGetValue(guid, out var prefab))
                {
                    var imageTransform = image.transform;
                    var spawnedPrefab = Instantiate(prefab, imageTransform.position, imageTransform.rotation);
                    m_SpawnedPrefabs[guid] = spawnedPrefab;
                    var numberBehavior = spawnedPrefab.GetComponent<NumberBehavior>();
                    if (numberBehavior != null)
                        m_SpawnedNumbers[guid] = numberBehavior;
                }
            }

            // updated, set prefab position and rotation
            foreach (var image in obj.updated)
            {
                var guid = image.referenceImage.guid;

                // If the image is tracking, update its position and show its visuals
                var isTracking = image.trackingState == TrackingState.Tracking;
                if (isTracking && m_SpawnedPrefabs.TryGetValue(guid, out var spawnedPrefab))
                {
                    var spawnedPrefabTransform = spawnedPrefab.transform;
                    var imageTransform = image.transform;
                    spawnedPrefabTransform.SetPositionAndRotation(imageTransform.position, imageTransform.rotation);
                }

                if (m_SpawnedNumbers.TryGetValue(guid, out var numberBehavior))
                {
                    numberBehavior.numberObject.SetActive(isTracking);
                }
            }

            // removed, destroy spawned instance
            foreach (var image in obj.removed)
            {
                var guid = image.referenceImage.guid;
                if (m_SpawnedPrefabs.TryGetValue(guid, out var spawnedPrefab))
                {
                    Destroy(spawnedPrefab);
                    m_SpawnedPrefabs.Remove(guid);
                    m_SpawnedNumbers.Remove(guid);
                }
            }
        }
#endif
    }
}
