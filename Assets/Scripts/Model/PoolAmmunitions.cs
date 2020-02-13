using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains 
{
    public sealed class PoolAmmunitions
    { 
        private Dictionary<AmmunitionType,PoolAmmunitionsStruct> _ammunitionPools;
        private int _countBulletsInPool = 50; //На данный момент максимальное количество существующих одновлеменно пуль
        private int _countRicochetInPool = 20;
        

        public PoolAmmunitions()
        {
            var bullet = Resources.Load<Bullet>(ResourceManager.BULLET);
            var ricochetBullet = Resources.Load<RicochetBullet>(ResourceManager.RICOCHETBULLET);
            _ammunitionPools = new Dictionary<AmmunitionType, PoolAmmunitionsStruct>();
            
            CreateAmmunitions(bullet, _countBulletsInPool);
            CreateAmmunitions(ricochetBullet, _countRicochetInPool);
        }
        
        private void CreateAmmunitions(Ammunition ammunition, int count = 1)
        {
            PoolAmmunitionsStruct pool = new PoolAmmunitionsStruct();
            for (int i = 0; i < count; i++)
            {
                
                Ammunition bullet = Object.Instantiate(ammunition);
                bullet.SetActive(false);
                pool.InsertBullets(bullet);
            }
            _ammunitionPools.Add(ammunition.Type, pool);
        }

        public Ammunition GetAmmunition(AmmunitionType type, Vector3 position, Quaternion rotation) 
        {
            Ammunition bullet = _ammunitionPools[type].GetAmmunition();
            if (bullet != null)
            {
                var transform = bullet.transform;
                transform.position = position;
                transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }

            return null;
        }

        public void InsertBullet(Ammunition ammunition)
        {
            //todo переделать
            _ammunitionPools[ammunition.Type].InsertBullets(ammunition);
            
        }
    }
}