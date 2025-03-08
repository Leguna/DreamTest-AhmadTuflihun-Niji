using Constant;
using EventStruct;
using LoadingModule;
using TopDownPlayer;
using TurnBasedCombat;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

[RequireComponent(typeof(LoadingManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private LoadingManager loadingManager;
    [SerializeField] private TopDownPlayerController playerPrefab;
    [SerializeField] private TurnBasedCombatController turnBasedCombatController;
    [SerializeField] private SceneIndexEnum defaultScene = SceneIndexEnum.ExploreScene;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameOver gameOver;

    private static GameState _gameState = GameState.Explore;

    protected override void Awake()
    {
        var newPlayer = Instantiate(playerPrefab);
        playerPrefab = newPlayer;
        GameConst.Init();
        if (loadingManager == null) TryGetComponent(out loadingManager);

        loadingManager.Init(defaultScene);
        SceneManager.LoadScene((int)SceneIndexEnum.BattleScene, LoadSceneMode.Additive);
        gameOver.onTryAgain += RestartGame;
    }

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        _gameState = SceneIndexEnum.ExploreScene == defaultScene ? GameState.Explore : GameState.Combat;
        playerPrefab.Init(_gameState);
        playerCamera = FindFirstObjectByType<PlayerCamera>();
    }

    private void OnEnable()
    {
        EventManager.AddEventListener<StateGameChanges>(ChangeGameState);
        EventManager.AddEventListener<StartTurnBasedGameEventData>(OnStartTurnBasedGame);
    }

    private void OnStartTurnBasedGame(StartTurnBasedGameEventData data)
    {
        ChangeGameState(new StateGameChanges(GameState.Battle));
    }

    private void OnDisable()
    {
        EventManager.RemoveEventListener<StateGameChanges>(ChangeGameState);
        EventManager.RemoveEventListener<StartTurnBasedGameEventData>(OnStartTurnBasedGame);
    }

    private void ChangeGameState(StateGameChanges state)
    {
        _gameState = state.gameState;

        switch (_gameState)
        {
            case GameState.Explore:
            case GameState.Combat:
                playerCamera.SetFollow(playerPrefab.transform);
                break;
            case GameState.Battle:
                playerCamera.StopFollow();
                break;
        }
    }


    public void RestartGame()
    {
        loadingManager.Init(defaultScene);
        turnBasedCombatController.Init();
        playerPrefab.Init(GameState.Explore);
    }
}


public enum GameState
{
    Pause,
    Loading,
    GameOver,
    Explore,
    Combat,
    Battle,
}