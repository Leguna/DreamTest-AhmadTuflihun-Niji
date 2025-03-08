using System;
using UnityEngine;
using Utilities;

namespace TurnBasedCombat.SO
{
    [Serializable]
    [CreateAssetMenu(fileName = "TurnBaseActorSo", menuName = "TurnBasedCombat/TurnBaseActorSo")]
    public class TurnBaseActorSo : BaseSo
    {
        public float speed = 3;
        public float speedMultiplier = 1;
        public int health = 1;
        public int maxHealth = 10;
        public int damage = 2;
        public int spellDamage = 1;
        public int defend = 1;

        public Sprite battleSprite;
        public Sprite iconQueue;
        public Color color;
        public Color teamColor;

        public void Rest()
        {
            health = maxHealth;
        }
    }
}