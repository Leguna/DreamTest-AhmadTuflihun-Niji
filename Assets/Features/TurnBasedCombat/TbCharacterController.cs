using System;
using DamageModule.HealthBar;
using TurnBasedCombat.SO;
using UnityEngine;

namespace TurnBasedCombat
{
    public class TbCharacterController : MonoBehaviour, IHealthBar
    {
        [SerializeField] protected HealthBarComponent healthBar;
        public TurnBaseActorSo actorData;
        public bool isActionSelected;

        public void Init(TurnBaseActorSo actor)
        {
            actorData = actor;
            CurrentHealth = actor.health;
            MaxHealth = actor.maxHealth;
            healthBar.Init(this, transform);
        }

        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public Color Color => actorData.teamColor;
        public bool IsDead => CurrentHealth <= 0;
    }
}