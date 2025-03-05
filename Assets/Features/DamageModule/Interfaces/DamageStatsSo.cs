using System;

namespace Features.DamageModule.Interfaces
{
    [Serializable]
    public class DamageStatsSo
    {
        public int maxHealth;
        public int currentHealth;
        public int defense;
        public int attack;
    }
}