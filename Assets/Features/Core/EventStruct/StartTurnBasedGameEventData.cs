using TurnBasedCombat;

namespace EventStruct
{
    public struct StartTurnBasedGameEventData
    {
        public readonly CombatStartType combatStartType;
        public readonly TurnBaseActorSo attackerSo;
        public readonly TurnBaseActorSo playerSo;

        public StartTurnBasedGameEventData(CombatStartType combatStartType, TurnBaseActorSo playerSo,
            TurnBaseActorSo attackerSo)
        {
            this.combatStartType = combatStartType;
            this.attackerSo = attackerSo;
            this.playerSo = playerSo;
        }
    }
}