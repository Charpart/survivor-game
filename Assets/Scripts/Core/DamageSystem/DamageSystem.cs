using System;
using System.Collections.Generic;

namespace Core.DamageSystem
{
    public class DamageSystem : IDisposable
    {
        private readonly List<IDamagable> _damagables = new List<IDamagable>(100);
        public IReadOnlyList<IDamagable> damagables => _damagables;
    
        public void Register(IDamagable damagable)
        {
            _damagables.Add(damagable);
        }
        
        public void Unregister(IDamagable damagable)
        {
            _damagables.Remove(damagable);
        }
        
        public void Dispose() { }
    }
}