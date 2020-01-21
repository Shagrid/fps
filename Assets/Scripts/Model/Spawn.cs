using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Geekbrains
{
    public sealed class Spawn : BaseObjectScene
    {
        [SerializeField] private Transform[] _spawns;
        [SerializeField] private Transform Enemy;
        private Transform _target;

        protected override void Awake()
        {
            base.Awake();
            _target = GameObject.FindGameObjectWithTag(TagManager.PLAYER).transform;
        }
        public void SpawnEnemies(int count)
        {
            int spawnIndex;
            Transform enemy;
            for (int i = 0; i < count; i++)
            {
                spawnIndex = Random.Range(0, _spawns.Length-1);
                enemy = Instantiate(Enemy, _spawns[spawnIndex]);
                enemy.GetComponent<AICharacterControl>().target = _target;
            }
           
        }
    }
}
