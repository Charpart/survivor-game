using Core.DamageSystem;
using UnityEngine;

namespace Core.ProjectileSystem
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _hitMask;
        [SerializeField] private float _radius;
        [SerializeField] private Transform _content;
        
        private readonly RaycastHit[] _hits = new RaycastHit[4];
        
        private Vector3 _rotationVelocity;
        
        private Vector3 _direction;
        private float _speed;
        private float _damage;

        public void Init(Vector3 dir, float speed, float damage)
        {
            _direction = dir;
            _speed = speed;
            _damage = damage;

            _rotationVelocity = Random.insideUnitSphere * 360.0f;
        }
        
        public void Update()
        {
            float deltaTime = Time.deltaTime;

            _content.transform.Rotate(_rotationVelocity * deltaTime);
            
            Vector3 oldPos = transform.position;
            Vector3 newPos = oldPos + _direction * (_speed * deltaTime);

            /*int hitCount = Physics.SphereCastNonAlloc(oldPos, _radius, _direction, _hits, _speed * deltaTime, _hitMask);

            for (int i = 0; i < hitCount; i++)
                HandleHit(_hits[i]);*/

            transform.position = newPos;
        }
        
        private void HandleHit(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out IDamagable enemy))
                enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}