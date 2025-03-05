﻿namespace Features.DamageModule.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Die();
    }
}