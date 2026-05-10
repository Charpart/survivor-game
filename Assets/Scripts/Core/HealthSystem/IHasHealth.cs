using R3;

namespace Core.HealthSystem
{
    public interface IHasHealth
    {
        public ReadOnlyReactiveProperty<float> health { get; }
        public ReadOnlyReactiveProperty<float> maxHealth { get; }
        
        public void SetHealth(float newHealth);
        public void SetMaxHealth(float newMaxHealth);
    }
}