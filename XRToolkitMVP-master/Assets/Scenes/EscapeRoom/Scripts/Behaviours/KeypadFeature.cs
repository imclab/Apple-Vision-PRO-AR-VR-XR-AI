using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeypadFeature : BaseFeature
{
    [SerializeField]
    private Button firstCodeButton;

    [SerializeField]
    private Button secondCodeButton;

    [SerializeField]
    private Button thirdCodeButton;

    [SerializeField]
    private GameObject keypadUI;

    [SerializeField]
    private UnityEvent OnKeycodesCorrect;

    [SerializeField]
    private int keycodesAnswer = 250;

    private int num1, num2, num3;

    protected override void Awake()
    {
        base.Awake();

        firstCodeButton.onClick.AddListener(() =>
        {
            num1++;
            if (num1 > 9) num1 = 0;
            firstCodeButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{num1}";
            CheckCodeCombination();
        });

        secondCodeButton.onClick.AddListener(() =>
        {
            num2++;
            if (num2 > 9) num2 = 0;
            secondCodeButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{num2}";
            CheckCodeCombination();
        });

        thirdCodeButton.onClick.AddListener(() =>
        {
            num3++;
            if (num3 > 9) num3 = 0;
            thirdCodeButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{num3}";
            CheckCodeCombination();
        });
    }
    private void CheckCodeCombination()
    {
        if (int.TryParse($"{num1}{num2}{num3}", out int code))
        {
            if(code == keycodesAnswer)
            {
                OnKeycodesCorrect?.Invoke();
                PlayOnEnded();
            }
            else
            {
                PlayOnStarted();
            }
        }
    }

    public void ToggleKeypad()
    {
        bool isActive = !keypadUI.activeSelf;
        keypadUI.SetActive(isActive);
    }
}
