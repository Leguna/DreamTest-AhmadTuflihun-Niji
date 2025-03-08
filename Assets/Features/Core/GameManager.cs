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
    [SerializeField] private TopDownPlayerController playerController;
    [SerializeField] private TurnBasedCombatController turnBasedCombatController;
    [SerializeField] private SceneIndexEnum defaultScene = SceneIndexEnum.ExploreScene;

    public static GameState gameState = GameState.Explore;

    protected override void Awake()
    {
        GameConst.Init();
        if (loadingManager == null) TryGetComponent(out loadingManager);
        if (turnBasedCombatController == null) TryGetComponent(out turnBasedCombatController);
        if (playerController == null) GameConst.playerObject.TryGetComponent(out playerController);
        loadingManager.Init(defaultScene);
        turnBasedCombatController.Init();
        gameState = SceneIndexEnum.ExploreScene == defaultScene ? GameState.Explore : GameState.Combat;
        playerController.Init(GameState.Explore);
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