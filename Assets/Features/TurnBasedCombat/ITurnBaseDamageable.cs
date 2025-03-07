using DamageModule.Interfaces;

namespace TurnBasedCombat
{
    public interface ITurnBaseDamageable
    {
        void TryTakeDamage(int damage, TurnBaseActorSo attacker);
    }
}