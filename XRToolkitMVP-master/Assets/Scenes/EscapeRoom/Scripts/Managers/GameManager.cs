using DilmerGames.Core.Singletons;
using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    public GameState GameState { get; private set; } = GameState.Playing;

    [Header("Linked Features")]
    [SerializeField]
    private PuzzleSolverFeature puzzleSolverFeature;

    [Header("Events")]
    public Action<GameState> onGamePaused;

    public Action<GameState> onGameResumed;

    public Action<GameState> onGameSolved;

    private LayerMask cachedCameraCullingMask;

    private void Awake()
    {
        cachedCameraCullingMask = Camera.main.cullingMask;
    }

    private void OnEnable()
    {
        ControllerManager.Instance.onControllerMenuActionExecuted += ToggleGameState;
        UIManager.Instance.onGameResumedActionExecuted += ToggleGameState;
        puzzleSolverFeature.onPuzzleSolved += GameSolved;
    }

    private void OnDisable()
    {
        ControllerManager.Instance.onControllerMenuActionExecuted -= ToggleGameState;
        UIManager.Instance.onGameResumedActionExecuted -= ToggleGameState;
        puzzleSolverFeature.onPuzzleSolved -= GameSolved;
    }

    private void GameSolved(GameState state)
    {
        GameState = state;
        CommitGameStateChanges();
    }
    
    private void ToggleGameState()
    {
        GameState = GameState == GameState.Playing ? GameState.Paused : GameState.Playing;
        CommitGameStateChanges();
    }

    private void CommitGameStateChanges()
    {
        if (GameState == GameState.Paused)
        {
            onGamePaused?.Invoke(GameState.Paused);
            Time.timeScale = 0;
            Camera.main.cullingMask = LayerMask.GetMask("UI");
        }
        else if(GameState == GameState.PuzzleSolved)
        {
            onGameSolved?.Invoke(GameState.PuzzleSolved);
            Time.timeScale = 0;
            Camera.main.cullingMask = LayerMask.GetMask("UI");
        }
        else
        {
            onGameResumed?.Invoke(GameState.Playing);
            Time.timeScale = 1;
            Camera.main.cullingMask = cachedCameraCullingMask;
        }
    }
}