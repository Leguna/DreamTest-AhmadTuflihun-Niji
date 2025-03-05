namespace Features.DamageModule.Interfaces
{
    public interface IHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
    }
}