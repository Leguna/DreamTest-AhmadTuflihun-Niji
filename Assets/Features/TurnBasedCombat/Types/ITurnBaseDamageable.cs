using DamageModule.Interfaces;
using TurnBasedCombat.SO;

namespace TurnBasedCombat
{
    public interface ITurnBaseDamageable
    {
        void TryTakeDamage(int damage, TurnBaseActorSo attacker);
    }
}