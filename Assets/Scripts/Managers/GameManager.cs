using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ChangeGameState(GameState.Menu);
    }

    public void ChangeGameState(GameState state)
    {
        Cursor.lockState = CursorLockMode.None;
        switch (state)
        {
            case GameState.Menu:
                LevelMananager.Instance.LoadScene("Menu");
                break;

            case GameState.Game:
                SwitchToMainScene();
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
        OnGameStateChanged?.Invoke(state);
    }

    public enum GameState
    {
        Menu,
        Game,
        Lose,
        Win 
    }
    
    public void SwitchToMainScene()
    {
        LevelMananager.Instance.LoadScene("MainScene");
    }
}
