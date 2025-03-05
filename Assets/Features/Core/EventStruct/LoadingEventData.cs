using System;
using System.Threading.Tasks;
using static LoadingModule.LoadingManager;

namespace EventStruct
{
    public struct LoadingEventData
    {
        public readonly Task task;
        public readonly string message;
        public readonly LoadingType loadingType;
        public readonly Action onComplete;

        public LoadingEventData(Task task, LoadingType loadingType = LoadingType.Overlay,
            Action onComplete = null, string message = "")
        {
            this.task = task;
            this.message = message;
            this.loadingType = loadingType;
            this.onComplete = onComplete;
        }
    }
}