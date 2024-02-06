using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolySpatial.Samples
{
    public class LevelBubbleManager : MonoBehaviour
    {
        [SerializeField] Transform m_BubbleRoot;

        [SerializeField] BubbleLayoutManager m_BubbleLayoutManager;

        [SerializeField] LevelData m_LevelData;

        [SerializeField] float m_RotateSpeed = 300.0f;

        [SerializeField] TMP_Text m_LevelTitle;

        [SerializeField] TMP_Text m_LevelDescription;

        List<BubbleSize> m_BubbleSizes;
        List<GameObject> m_BubbleObjects;
        List<BubbleCircleNode> m_BubbleCircleNodes;
        float m_StartTime;
        float m_RotationLength;
        int m_CurrentSelectedIndex;
        Vector3 m_TargetRotation;
        Vector3 m_PreviousRotation;
        const float k_StartingOffset = 180.0f;

        void Start()
        {
            m_BubbleSizes = new List<BubbleSize>();
            m_BubbleObjects = new List<GameObject>();

            foreach (Transform bubbles in m_BubbleRoot)
            {
                m_BubbleSizes.Add(bubbles.GetComponent<BubbleSize>());
                m_BubbleObjects.Add(bubbles.gameObject);
            }

            UpdateLevelInfo();
            MakeBubbleCircle();
            SetBubbleScale();
            m_TargetRotation = new Vector3(0, k_StartingOffset, 0);
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                ArrowButtonPressed(true);
                m_StartTime = Time.time;
                m_RotationLength = m_BubbleLayoutManager.BubbleSpacing;
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                ArrowButtonPressed(false);
                m_StartTime = Time.time;
                m_RotationLength = m_BubbleLayoutManager.BubbleSpacing;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                LoadSelectedLevel();
            }
#endif

            var distanceCovered = (Time.time - m_StartTime) * m_RotateSpeed;
            var fractionOfRotation = distanceCovered / m_RotationLength;

            if (distanceCovered > 0)
            {
                m_BubbleRoot.transform.eulerAngles = Vector3.Lerp(m_PreviousRotation, m_TargetRotation, fractionOfRotation);
            }
        }

        public void ArrowButtonPressed(bool left)
        {
            m_StartTime = Time.time;
            m_RotationLength = m_BubbleLayoutManager.BubbleSpacing;

            var direction = left ? 1 : -1;
            m_PreviousRotation = m_TargetRotation;
            m_TargetRotation += new Vector3(0, direction * m_BubbleLayoutManager.BubbleSpacing, 0);

            // cycle index around 0 depending on which button was pressed
            if (m_CurrentSelectedIndex == m_BubbleSizes.Count - 1 && !left)
            {
                m_CurrentSelectedIndex = 0;
            }
            else
            {
                m_CurrentSelectedIndex -= direction;
                if (m_CurrentSelectedIndex < 0)
                {
                    m_CurrentSelectedIndex = m_BubbleSizes.Count - 1;
                }

                if (m_CurrentSelectedIndex > m_BubbleSizes.Count)
                {
                    m_CurrentSelectedIndex = 0;
                }
            }

            UpdateLevelInfo();
            SetBubbleScale();
        }

        void SetBubbleScale()
        {
            // large
            var currentSelection = m_BubbleSizes[m_CurrentSelectedIndex];
            // medium
            var nextBubble = m_BubbleCircleNodes[m_CurrentSelectedIndex].NextBubble;
            var previousBubble = m_BubbleCircleNodes[m_CurrentSelectedIndex].PreviousBubble;
            // small
            GameObject nextNextBubble = null;
            GameObject previousPreviousBubble = null;
            foreach (var bubbleNode in m_BubbleCircleNodes)
            {
                if (bubbleNode.Bubble == nextBubble)
                {
                    nextNextBubble = bubbleNode.NextBubble;
                }

                if (bubbleNode.Bubble == previousBubble)
                {
                    previousPreviousBubble = bubbleNode.PreviousBubble;
                }

                // all others are set to extra small
                if (bubbleNode.Bubble != nextBubble || bubbleNode.Bubble != previousBubble || bubbleNode.Bubble != nextNextBubble ||
                    bubbleNode.Bubble != previousPreviousBubble || bubbleNode.Bubble != currentSelection.gameObject)
                {
                    bubbleNode.Bubble.GetComponent<BubbleSize>().SetScale(BubbleSize.BubbleSizeEnum.ExtraSmall);
                }
            }

            // set scale
            currentSelection.SetScale(BubbleSize.BubbleSizeEnum.Large);
            nextBubble.GetComponent<BubbleSize>().SetScale(BubbleSize.BubbleSizeEnum.Medium);
            previousBubble.GetComponent<BubbleSize>().SetScale(BubbleSize.BubbleSizeEnum.Medium);
            nextNextBubble.GetComponent<BubbleSize>().SetScale(BubbleSize.BubbleSizeEnum.Small);
            previousPreviousBubble.GetComponent<BubbleSize>().SetScale(BubbleSize.BubbleSizeEnum.Small);
        }

        public void LoadSelectedLevel()
        {
            m_BubbleObjects[m_CurrentSelectedIndex].GetComponent<LoadLevelButton>().Press();
        }

        void MakeBubbleCircle()
        {
            m_BubbleCircleNodes = new List<BubbleCircleNode>();

            for (int i = 0; i < m_BubbleObjects.Count; i++)
            {
                var nextIndex = i + 1;
                var previousIndex = i - 1;

                if (nextIndex > m_BubbleObjects.Count - 1)
                {
                    nextIndex = 0;
                }

                if (previousIndex < 0)
                {
                    previousIndex = m_BubbleObjects.Count - 1;
                }

                var nextBubble = m_BubbleObjects[nextIndex];
                var previousBubble = m_BubbleObjects[previousIndex];
                var newBubbleNode = new BubbleCircleNode(m_BubbleObjects[i], nextBubble, previousBubble);
                m_BubbleCircleNodes.Add(newBubbleNode);
            }
        }

        void UpdateLevelInfo()
        {
            var levelType = m_BubbleObjects[m_CurrentSelectedIndex].GetComponent<LoadLevelButton>().LevelType;
            m_LevelTitle.text = m_LevelData.GetLevelTitle(levelType);
            m_LevelDescription.text = m_LevelData.GetLevelDescription(levelType);
        }
    }

    struct BubbleCircleNode
    {
        public GameObject Bubble;
        public GameObject NextBubble;
        public GameObject PreviousBubble;

        public BubbleCircleNode(GameObject bubble, GameObject nextBubble, GameObject previousBubble)
        {
            Bubble = bubble;
            NextBubble = nextBubble;
            PreviousBubble = previousBubble;
        }
    }
}
