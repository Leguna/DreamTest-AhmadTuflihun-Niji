namespace Features.DamageModule.Interfaces
{
    public interface IWeaponController : IWeapon
    {
        void DoDamage(IDamageable target);
    }
}