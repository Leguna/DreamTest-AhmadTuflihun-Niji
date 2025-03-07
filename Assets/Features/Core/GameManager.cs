using Constant;
using LoadingModule;
using UnityEngine;

[RequireComponent(typeof(LoadingManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private LoadingManager loadingManager;

    public GameState gameState = GameState.Explore;

    private void Awake()
    {
        GameConst.Init();
        if (loadingManager == null) TryGetComponent(out loadingManager);
        loadingManager.Init();
    }
}


public enum GameState
{
    Pause,
    Loading,
    Explore,
    Crawling,
    Combat,
}