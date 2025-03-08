using Facing;

namespace TurnBasedCombat
{
    public enum CombatStartType
    {
        Advantage,
        Ambush,
        Neutral
    }

    public static class CombatStartTypeExtensions
    {
        public static CombatStartType GetStartTypeBySide(this Side side)
        {
            // From back is advantage for player, side and front is neutral else ambush
            return side switch
            {
                Side.Front => CombatStartType.Neutral,
                Side.Side => CombatStartType.Neutral,
                Side.Back => CombatStartType.Advantage,
                _ => CombatStartType.Ambush
            };
        }
    }
}