using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [field: SerializeField]
    public Button ResumeButton { get; set; }

    [field: SerializeField]
    public Button RestartButton { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI SolvedText { get; set; }
}
