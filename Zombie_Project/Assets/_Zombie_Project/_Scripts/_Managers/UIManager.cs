using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuScreen;
    public GameObject gamePlayScreen;
    public GameObject gameOverScreen;

    public TMP_Text scoreDisplay;
    public TMP_Text totalScoreDisplay;

    int totalScore;


    public List<GameObject> allUIScreens = new List<GameObject>();

    private void OnEnable()
    {
        ShowMainMenu();

        EventManager.AddScoreToPlayer += AddScoreToPlayer;
        EventManager.GameEnded += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.AddScoreToPlayer -= AddScoreToPlayer;
        EventManager.GameEnded -= OnGameOver;
    }

    void ShowMainMenu()
    {
        DisableAllScreens();
        mainMenuScreen.SetActive(true);
    }

    void ShowGamePlayScreen()
    {
        DisableAllScreens();
        gamePlayScreen.SetActive(true);
    }

    void ShowGameOverScreen()
    {
        DisableAllScreens();
        gameOverScreen.SetActive(true);
        totalScoreDisplay.text = totalScore.ToString();
    }

    void DisableAllScreens()
    {
        for (int i = 0; i < allUIScreens.Count; i++)
        {
            allUIScreens[i].SetActive(false);
        }
    }

    public void StartGameClicked()
    {
        ShowGamePlayScreen();
        EventManager.GameStarted?.Invoke();
    }

    void OnGameOver()
    {
        ShowGameOverScreen();
    }

    public void OnRestartGameCicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void AddScoreToPlayer(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        scoreDisplay.text = totalScore.ToString();
    }
}
