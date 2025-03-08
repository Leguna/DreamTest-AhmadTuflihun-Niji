using System;
using EventStruct;
using UnityEngine;
using Utilities;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    public Action onTryAgain;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.AddEventListener<GameOverEventData>(ShowGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveEventListener<GameOverEventData>(ShowGameOver);
    }

    private void ShowGameOver(GameOverEventData data)
    {
        gameOverPanel.SetActive(true);
        onTryAgain = data.onTryAgain;
    }

    public void TryAgain()
    {
        onTryAgain?.Invoke();
        gameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}