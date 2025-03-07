using System;
using UnityEngine;
using Utilities;

namespace TurnBasedCombat
{
    [Serializable]
    [CreateAssetMenu(fileName = "TurnBaseActorSo", menuName = "TurnBasedCombat/TurnBaseActorSo")]
    public class TurnBaseActorSo : BaseSo
    {
        public float speed = 3;
        public int health = 1;
        public int damage = 2;
        public int defend;
        public int magicDamage = 1;
        public int magicDefend;
    }
}