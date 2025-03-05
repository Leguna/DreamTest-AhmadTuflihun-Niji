using System;
using Features.DamageModule.Interfaces;
using UnityEngine;

namespace Features.DamageModule.HealthBar
{
    public interface IHealthBar : IHealth
    {
        public event Action<int> OnHealTaken;
        public event Action<int> OnDamageTaken;
        public Color Color { get; }
    }
}