﻿using UnityEngine;

namespace DamageModule.Interfaces
{
    public interface IDamageable
    {
        void TryTakeDamage(int damage);
    }

    public interface IDamageable<in T>
    {
        void TryTakeDamage(int damage, GameObject attacker, T attackerSo);
    }
}