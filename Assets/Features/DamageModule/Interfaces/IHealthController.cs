using DamageModule.Interfaces;

namespace Features.DamageModule.Interfaces
{
    public interface IHealthController : IHealth, IDamageable, IHealable
    {
    }
}