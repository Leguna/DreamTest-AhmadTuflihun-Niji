using System;

namespace EventStruct
{
    public struct GameOverEventData
    {
        public Action onTryAgain;

        public GameOverEventData(Action onTryAgain)
        {
            this.onTryAgain = onTryAgain;
        }
    }
}