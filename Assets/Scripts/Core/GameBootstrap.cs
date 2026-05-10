using System;
using Core.Spawners;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameBootstrap : IInitializable, IDisposable
    {
        private PlayerSpawner _playerSpawner;
        private EnemySpawner _enemySpawner;
        private CameraFollow _cameraFollow;
        
        [Inject]
        private void Construct(
            PlayerSpawner playerSpawner, 
            EnemySpawner enemySpawner,
            CameraFollow cameraFollow)
        {
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _cameraFollow = cameraFollow;
        }
        
        public void Initialize()
        {
            var player = _playerSpawner.Spawn();
            _cameraFollow.SetTarget(player.transform);
            _enemySpawner.Construct(player.transform);
        }

        public void Dispose() { }
    }
}