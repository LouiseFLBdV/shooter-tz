using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    public Text enemyCount;
    public Text textProgressStatus;
    public Text textWinnings;
    public Text textLosses;

    private int winnings;
    private int losses;

    private void Start()
    {
        winnings = PlayerPrefs.GetInt("Winnings");
        losses = PlayerPrefs.GetInt("Losses");
        FindEnemies();
        UpdateEnemyCountText();
    }

    private void FindEnemies()
    {
        foreach (Transform enemy in transform)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemies.Add(enemy.gameObject);
            }
        }
    }

    private void UpdateEnemyCountText()
    {
        enemyCount.text = "To win, defeat: " + enemies.Count;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        UpdateEnemyCountText();
        if (enemies.Count == 0)
        {
            HandleWin();
        }
    }

    private void HandleWin()
    {
        winnings++;
        PlayerPrefs.SetInt("Winnings", winnings);
        RenderStatusText();
        textProgressStatus.text = "YOU WIN";
        GameManager.Instance.ChangeGameState(GameManager.GameState.Win);
    }

    public void HandleLose()
    {
        losses++;
        PlayerPrefs.SetInt("Losses", losses);
        RenderStatusText();
        textProgressStatus.text = "WASTED =)";
        GameManager.Instance.ChangeGameState(GameManager.GameState.Lose);
    }

    private void RenderStatusText()
    {
        textWinnings.text = "Winnings: " + winnings;
        textLosses.text = "Losses: " + losses;
    }
}
