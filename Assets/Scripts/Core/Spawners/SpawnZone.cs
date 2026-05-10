using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Spawners
{
    [RequireComponent(typeof(BoxCollider))]
    public class SpawnZone : MonoBehaviour
    {
        [SerializeField] private SpawnZoneService _spawnZoneService;
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private List<SpawnZone> _neighbors;
        
        public IReadOnlyList<SpawnZone> neighbors => _neighbors;
        public BoxCollider boxCollider => _boxCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            _spawnZoneService.TriggerEnter(this, other);
        }

        private void OnTriggerExit(Collider other)
        {
            _spawnZoneService.TriggerExit(this, other);
        }
        
#if UNITY_EDITOR
        [ContextMenu("Attach collider")]
        private void AttachCollider()
        {
            _boxCollider = GetComponent<BoxCollider>();            
            EditorUtility.SetDirty(this);
        }
        
        [ContextMenu("Fill Neighbors")]
        private void FillNeighbors()
        {
            var bounds = _boxCollider.bounds;
            bounds.Expand(0.1f);

            var allSpawnZones = 
                FindObjectsByType<SpawnZone>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            _neighbors = allSpawnZones.Where(col => bounds.Intersects(col._boxCollider.bounds)).ToList();
            _neighbors.Remove(this);
            
            EditorUtility.SetDirty(this);
        }
#endif
    }
}