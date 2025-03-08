using Constant;
using EventStruct;
using LoadingModule;
using TopDownPlayer;
using TurnBasedCombat;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(LoadingManager))]
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private LoadingManager loadingManager;
    [SerializeField] private TopDownPlayerController playerPrefab;
     private TurnBasedCombatController _turnBasedCombatController;
    [SerializeField] private SceneIndexEnum defaultScene = SceneIndexEnum.ExploreScene;

    public static GameState gameState = GameState.Explore;

    protected override void Awake()
    {
        gameState = SceneIndexEnum.ExploreScene == defaultScene ? GameState.Explore : GameState.Combat;
        var newPlayer = Instantiate(playerPrefab);
        playerPrefab = newPlayer;        
        playerPrefab.Init(gameState);
 
        GameConst.Init();
        if (loadingManager == null) TryGetComponent(out loadingManager);
        if (_turnBasedCombatController == null) TryGetComponent(out _turnBasedCombatController);
        loadingManager.Init(defaultScene);
        _turnBasedCombatController.Init();
    }

    private void OnEnable()
    {
        EventManager.AddEventListener<StateGameChanges>(ChangeGameState);
    }

    private void OnDisable()
    {
        EventManager.RemoveEventListener<StateGameChanges>(ChangeGameState);
    }

    private void ChangeGameState(StateGameChanges state)
    {
        gameState = state.gameState;
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