using System;
using TurnBasedCombat;

namespace EventStruct
{
    public struct FinishTurnBasedGameEventData
    {
        public readonly FinishType finishType;

        public FinishTurnBasedGameEventData(FinishType finishType)
        {
            this.finishType = finishType;
        }
    }
}