using EventStruct;
using UnityEngine;
using Utilities;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

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
    }

    public void TryAgain()
    {
        GameManager.Instance.RestartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}