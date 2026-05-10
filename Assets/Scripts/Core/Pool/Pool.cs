using System.Collections.Generic;
using UnityEngine;

namespace Core.Pool
{
    public class Pool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _pool = new Queue<T>();
    
        public Pool(T prefab, int preloadCount, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        
            for (int i = 0; i < preloadCount; i++)
                Create();
        }
    
        private T Create()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            return obj;
        }
    
        public T Pop(Vector3 position, Quaternion rotation)
        {
            T obj = _pool.Count > 0 ? _pool.Dequeue() : Create();
        
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.gameObject.SetActive(true);
        
            return obj;
        }
    
        public void Push(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}