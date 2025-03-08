using System;
using Features.DamageModule.Interfaces;
using UnityEngine;

namespace DamageModule.HealthBar
{
    public interface IHealthBar : IHealth
    {
        public Color Color { get; }
    }
}