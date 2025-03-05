using System;
using Features.DamageModule.HealthBar;
using Features.DamageModule.Interfaces;
using UnityEngine;

namespace Features.DamageModule
{
    [Serializable]
    public class HealthController : MonoBehaviour, IHealthController, IHealthBar
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public event Action<int> OnHealTaken;
        public event Action<int> OnDamageTaken;
        public Color Color { get; set; }

        private Action _onDeath;


        public void Init(int maxHealth, Color color)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Color = color;
        }

        public HealthController(int maxHealth, Color color)
        {
            Init(maxHealth, color);
        }

        public void SetListener(Action<int> onDamageTaken, Action<int> onHealTaken, Action onDeath)
        {
            OnDamageTaken = onDamageTaken;
            OnHealTaken = onHealTaken;
            _onDeath = onDeath;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            OnDamageTaken?.Invoke(damage);
            if (CurrentHealth <= 0) Die();
        }

        public void Die()
        {
            CurrentHealth = 0;
            _onDeath?.Invoke();
        }

        public void Heal(int heal)
        {
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
            OnHealTaken?.Invoke(heal);
        }

        public void FullHeal()
        {
            CurrentHealth = MaxHealth;
            OnHealTaken?.Invoke(MaxHealth);
        }
    }
}