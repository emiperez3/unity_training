using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] Text gemsCountText, highScoreText;
    [SerializeField] GameObject gameOverMenu;

    public void SetHighScore(int highScore)
    {
        highScoreText.text = "HIGHSCORE: " + highScore;
    }

    public void UpdateGems(int amount)
    {
        gemsCountText.text = "GEMS: " + amount;
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
