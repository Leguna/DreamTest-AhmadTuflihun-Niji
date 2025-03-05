using Constant;

namespace EventStruct
{
    public struct LoadSceneEventData
    {
        public readonly SceneIndexEnum sceneIndexEnum;
        public readonly bool replaceCurrentScene;
        public LoadSceneEventData(SceneIndexEnum sceneIndexEnum, bool replaceCurrentScene = false)
        {
            this.sceneIndexEnum = sceneIndexEnum;
            this.replaceCurrentScene = replaceCurrentScene;
        }
    }
}