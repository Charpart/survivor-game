using System.Collections.Generic;
using UnityEngine;

namespace Core.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnZoneService _spawnZoneService;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _spawnCount = 10;

        private readonly List<Vector3> _cachedSpawnPoints = new(50);
        
        private Transform _player;
        private float _timer;

        public void Construct(Transform player)
        {
            _player = player;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0f;
                _cachedSpawnPoints.Clear();
                _spawnZoneService.TryGetSpawnPointsInZone(in _cachedSpawnPoints, _spawnCount);
                for (int i = 0; i < _cachedSpawnPoints.Count; i++)
                    SpawnEnemy(_cachedSpawnPoints[i]);
            }
        }

        private void SpawnEnemy(Vector3 pos)
        {
            Enemy e = Instantiate(_enemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>();
            e.Construct(_player.GetComponent<PlayerController>());
        }
    }
}