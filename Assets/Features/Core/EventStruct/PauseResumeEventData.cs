namespace EventStruct
{
    public struct PauseResumeEventData
    {
        public readonly bool isPause;
        
        public PauseResumeEventData(bool isPause)
        {
            this.isPause = isPause;
        }
    }
}