using System;
using System.Collections.Generic;

namespace Constant
{
    [Serializable]
    public class SceneConst
    {
        public static int GetSceneIndex(SceneIndexEnum sceneIndexEnum)
        {
            return (int)sceneIndexEnum;
        }

        public static string GetSceneName(SceneIndexEnum sceneIndexEnum)
        {
            return sceneIndexEnum.ToString();
        }

        public static readonly List<SceneIndexEnum> SpawnableScene = new()
        {
            SceneIndexEnum.CombatScene,
            SceneIndexEnum.ExploreScene
        };

        public static bool IsSceneSpawnable(SceneIndexEnum sceneIndexEnum)
        {
            return SpawnableScene.Contains(sceneIndexEnum);
        }
    }
    [Serializable]
    public enum SceneIndexEnum
    {
        MainScene = 0,
        LoadingScene = 1,
        ExploreScene = 2,
        CombatScene = 3,
        BattleScene = 4,
    }
}