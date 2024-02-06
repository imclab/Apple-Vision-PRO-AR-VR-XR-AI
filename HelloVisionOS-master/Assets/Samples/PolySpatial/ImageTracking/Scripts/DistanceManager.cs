using UnityEngine;

namespace PolySpatial.Samples
{
    public class DistanceManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Image Tracking manager that detects tracked images")]
        ImageTrackingObjectManager m_ImageTrackingObjectManager;

        [SerializeField]
        [Tooltip("Prefab to be spawned and showed between numbers based on distance")]
        GameObject m_SumPrefab;

        [SerializeField]
        [Tooltip("Distance at which to spawn the prefab")]
        float m_SumDistance = 0.3f;

        GameObject m_SpawnedSumPrefab;

        void Start()
        {
            m_SpawnedSumPrefab = Instantiate(m_SumPrefab, Vector3.zero, Quaternion.identity);
            m_SpawnedSumPrefab.SetActive(false);
        }

        void Update()
        {
            var spawnedPrefabs = m_ImageTrackingObjectManager.spawnedPrefabs;
            if (spawnedPrefabs.Count > 1)
            {
                GameObject first = null;
                GameObject second = null;
                foreach (var kvp in spawnedPrefabs)
                {
                    if (first == null)
                        first = kvp.Value;
                    else if (second == null)
                        second = kvp.Value;
                    else
                        break;
                }

                if (first == null || second == null)
                    return;

                var firstPosition = first.transform.position;
                var secondPosition = second.transform.position;
                var distance = Vector3.Distance(firstPosition, secondPosition);

                if (distance <= m_SumDistance)
                {
                    if (!m_SpawnedSumPrefab.activeSelf)
                        m_SpawnedSumPrefab.SetActive(true);

                    m_SpawnedSumPrefab.transform.position = (firstPosition + secondPosition) / 2;
                }
                else
                {
                    m_SpawnedSumPrefab.SetActive(false);
                }
            }
            else
            {
                m_SpawnedSumPrefab.SetActive(false);
            }
        }
    }
}
