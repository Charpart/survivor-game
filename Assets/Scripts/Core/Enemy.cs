using Core.DamageSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3.5f;
        
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackCooldown = 1f;
        
        private float _pathTimer;
        private float _damageTimer;

        private NavMeshAgent _agent;
        private PlayerController _player;

        public void Construct(PlayerController player)
        {
            _player = player;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _moveSpeed;
        }

        private void Update()
        {
            if (!_player) return;

            _pathTimer -= Time.deltaTime;
            if (_pathTimer <= 0f)
            {
                _pathTimer = 0.25f;
                _agent.SetDestination(_player.transform.position);
            }
            
            float dist = Vector3.Distance(transform.position, _player.transform.position);
            if (dist <= _attackRange)
            {
                _damageTimer -= Time.deltaTime;

                if (_damageTimer <= 0f)
                {
                    _damageTimer = _attackCooldown;
                    //_player.TakeDamage(_damage);
                }
            }
        }
    }
}