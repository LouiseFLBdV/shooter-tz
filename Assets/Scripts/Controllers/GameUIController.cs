using UnityEngine;

public class GameUIController : MonoBehaviour
{
    
    public GameObject popup;

    void Start()
    {
        GameManager.OnGameStateChanged += HandleStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleStateChange;
    }

    public void HandleMenuButton()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Menu);
    }
    
    public void HandleStateChange(GameManager.GameState state)
    {
        string text = null;
        if (state == GameManager.GameState.Win)
        {
            text = "Win";
        }
        else if (state == GameManager.GameState.Lose)
        {
            text = "Lose";
        }
        popup.SetActive(text != null);
    }
}