using UnityEngine;

namespace Geekbrains
{
    public sealed class SpawnController : BaseController, IExecute
    {
        private Spawn _spawn;
        private int _minSpawnEnemies = 1;
        private int _maxSpawnEnemies = 4;
        private float _timeToRespawn = 10f;
        private float _respownTimer;

        public void Execute()
        {
            if (!IsActive) return;

            _respownTimer -= Time.deltaTime;
            if (_respownTimer <= 0)
            {
                _spawn.SpawnEnemies(Random.Range(_minSpawnEnemies, _maxSpawnEnemies));
                _respownTimer = _timeToRespawn;
            }
            
        }

        public SpawnController(Spawn spawn)
        {
            _spawn = spawn;
        }

       

        
    }
}
