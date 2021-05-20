using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int gemsCollected;
    private ScreenManager screenManager;
    int currentHighscore;

    void Start()
    {
        currentHighscore = PlayerPrefs.GetInt("highscore", 0);

        screenManager = FindObjectOfType<ScreenManager>();
        screenManager.SetHighScore(currentHighscore);
        screenManager.UpdateGems(gemsCollected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGemCollected()
    {
        gemsCollected++;
        screenManager.UpdateGems(gemsCollected);
    }

    public void OnPlayerDeath()
    {
        if (gemsCollected > currentHighscore)
        {
            currentHighscore = gemsCollected;
            screenManager.SetHighScore(currentHighscore);
            PlayerPrefs.SetInt("highscore", currentHighscore);
        }

        screenManager.ShowGameOverMenu();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
