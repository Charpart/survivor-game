using R3;
using UnityEngine;

namespace Core.HealthSystem
{
    public class HealthComponent : MonoBehaviour, IHasHealth
    {
        [SerializeField] private float _health = 100;
        [SerializeField] private float _maxHealth = 100;
        
        private ReactiveProperty<float> _healthProperty = new();
        private ReactiveProperty<float> _maxHealthProperty = new();

        public ReadOnlyReactiveProperty<float> health => _healthProperty;
        public ReadOnlyReactiveProperty<float> maxHealth => _maxHealthProperty;

        private void Start()
        {
            _healthProperty = new ReactiveProperty<float>(_health);
            _maxHealthProperty = new ReactiveProperty<float>(_maxHealth);
        }

        public void SetHealth(float newHealth)
        {
            _healthProperty.Value = newHealth;
        }

        public void SetMaxHealth(float newMaxHealth)
        {
            _maxHealthProperty.Value = newMaxHealth;
        }
    }
}