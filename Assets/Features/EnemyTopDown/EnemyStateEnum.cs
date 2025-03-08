namespace EnemyTopDown
{
    public partial class AIEnemy
    {
        private enum EnemyState
        {
            Idle,
            Roam,
            Chase,
            Stun,
            CanAttack,
            TryAttack,
            Paused
        }
    }
}