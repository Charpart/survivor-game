using Core;
using Core.Spawners;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Initialization
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cameraFollow);
            builder.RegisterInstance(_playerSpawner);
            builder.RegisterInstance(_enemySpawner);
            
            builder.RegisterEntryPoint<GameBootstrap>();
            builder.RegisterEntryPointExceptionHandler(Debug.LogException);
        }
    }
}