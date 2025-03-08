using TurnBasedCombat;

namespace EventStruct
{
    public struct StartTurnBasedGameEventData
    {
        public readonly CombatStartType startType;
        public readonly TurnBaseActorSo attackerData;

        public StartTurnBasedGameEventData(CombatStartType startType, TurnBaseActorSo attackerData)
        {
            this.startType = startType;
            this.attackerData = attackerData;
        }

        public override string ToString()
        {
            return $"StartType: {startType}, AttackerData: {attackerData}";
        }
    }
}