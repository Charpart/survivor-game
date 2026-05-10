using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Spawners
{
    public class SpawnZoneService : MonoBehaviour
    {
        [SerializeField] private string _observedTag;
        private SpawnZone _observedInThatZone;

        private readonly List<SpawnZone> _cacheAvaliableZone = new(10);
        
        public void TriggerEnter(SpawnZone zone, Collider col)
        {
            if (col.CompareTag(_observedTag))
                _observedInThatZone = zone;
        }

        public void TriggerExit(SpawnZone zone, Collider col)
        {
            
        }
        
        public void TryGetSpawnPointsInZone(in List<Vector3> spawnPoints, int count)
        {
            if (!_observedInThatZone)
            {
                Debug.LogWarning("There is no player in any of the zones");
                return;
            }
            
            _cacheAvaliableZone.Clear();
            _cacheAvaliableZone.AddRange(_observedInThatZone.neighbors);

            var rndNearZone = _cacheAvaliableZone[Random.Range(0, _cacheAvaliableZone.Count)];
            var zoneNeighbours = rndNearZone.neighbors;
            _cacheAvaliableZone.Clear();
            _cacheAvaliableZone.AddRange(zoneNeighbours);
            _cacheAvaliableZone.RemoveAll(zone => _observedInThatZone.neighbors.Contains(zone));
            _cacheAvaliableZone.Remove(_observedInThatZone);

            SpawnZone finalZone = rndNearZone;
            if (_cacheAvaliableZone.Count > 0) 
                finalZone = _cacheAvaliableZone[Random.Range(0, _cacheAvaliableZone.Count)];
            
            for (int i = 0; i < count; i++)
                spawnPoints.Add(GetRandomPointInZone(finalZone));
        }
        
        private Vector3 GetRandomPointInZone(SpawnZone spawnZone)
        {
            BoxCollider box = spawnZone.boxCollider;
            Bounds bounds = box.bounds;
    
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}