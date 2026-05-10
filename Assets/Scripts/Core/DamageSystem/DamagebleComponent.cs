using Core.HealthSystem;
using UnityEngine;

namespace Core.DamageSystem
{
    [RequireComponent(typeof(HealthComponent))]
    public class DamagebleComponent : MonoBehaviour, IDamagable
    {
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        public void TakeDamage(float damage)
        {
            var diff = _healthComponent.health.CurrentValue - damage;
            _healthComponent.SetHealth(diff);
            
            if (_healthComponent.health.CurrentValue <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}