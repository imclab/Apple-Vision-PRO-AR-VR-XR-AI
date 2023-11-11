using DilmerGames.Core.Singletons;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Options")]
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f;

    [SerializeField]
    private GameObject menuContainer;

    private const string GAME_SCENE_NAME = "Game";

    [Header("Events")]
    public Action onGameResumedActionExecuted;

    private Menu menu;

    public void Awake()
    {
        // bind menu buttons
        menu = menuContainer.GetComponentInChildren<Menu>(true);

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuOptions(GameState.Playing);
            onGameResumedActionExecuted?.Invoke();
        });

        menu.RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
            onGameResumedActionExecuted?.Invoke();
        });
    }

    private void OnEnable()
    {
        // bind to game manager events
        GameManager.Instance.onGamePaused += HandleMenuOptions;
        GameManager.Instance.onGameResumed += HandleMenuOptions;
        GameManager.Instance.onGameSolved += HandleMenuOptions;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGamePaused -= HandleMenuOptions;
        GameManager.Instance.onGameResumed -= HandleMenuOptions;
        GameManager.Instance.onGameSolved -= HandleMenuOptions;
    }

    private void HandleMenuOptions(GameState gameState)
    {
        if (gameState == GameState.Paused)
        {
            menuContainer.SetActive(true);
            PlaceMenuInFrontOfPlayer();
        }
        else if(gameState == GameState.PuzzleSolved)
        {
            menuContainer.SetActive(true);
            menu.ResumeButton.gameObject.SetActive(false);
            menu.SolvedText.gameObject.SetActive(true);
            PlaceMenuInFrontOfPlayer();
        }
        else
        {
            menuContainer.SetActive(false);
        }
    }

    private void PlaceMenuInFrontOfPlayer()
    {
        // place UI in front of the player
        var playerHead = Camera.main.transform;
        menuContainer.transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        menuContainer.transform.rotation = playerHead.rotation;
    }
}
