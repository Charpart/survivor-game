using System.Collections.Generic;
using UnityEngine;

namespace Core.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabs;
        [SerializeField] private List<Transform> _spawnPoints;
        
        public PlayerController Spawn()
        {
            var instance = Instantiate(_prefabs, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            return instance.GetComponent<PlayerController>();
        }
    }
}