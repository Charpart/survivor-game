using UnityEngine;

namespace Core.ProjectileSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _delayBetweenShot = 0.5f;
        [SerializeField] private float _shootDistance = 20f;
        
        [SerializeField] private LayerMask _obstacleLayers = -1;
        [SerializeField] private string _targetTag = "Enemy";

        [SerializeField] private float _speed = 25.0f;
        [SerializeField] private float _damage = 5.0f;
        
        private float _nextShootTime;
        private Transform _currentTarget;
        
        private void Update()
        {
            FindTarget();
            if (CanShoot()) Shoot();
        }
        
        private bool CanShoot()
        {
            return _currentTarget && Time.time >= _nextShootTime;
        }
        
        private void FindTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(_targetTag);
            
            float closestDistance = _shootDistance;
            Transform closestTarget = null;
            
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance && IsInLineOfSight(enemy.transform))
                {
                    closestDistance = distance;
                    closestTarget = enemy.transform;
                }
            }
            
            _currentTarget = closestTarget;
        }
        
        private bool IsInLineOfSight(Transform target)
        {
            Vector3 direction = target.position - _firePoint.position;
            return !Physics.Raycast(_firePoint.position, direction, out _, _shootDistance, _obstacleLayers);
        }
        
        private void Shoot()
        {
            _nextShootTime = Time.time + _delayBetweenShot;
            
            Vector3 direction = (_currentTarget.position - _firePoint.position).normalized;
            _firePoint.rotation = Quaternion.LookRotation(direction);
            
            var projectile = 
                Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation).GetComponent<Projectile>();
            
            projectile.Init(direction, _speed, _damage);
        }
    }
}