using System;
using UnityEngine;

namespace Geekbrains
{
    public class PoolAmmunitions : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private RicochetBullet _ricochetBullet;
        private PoolBullets _bullets;
        private PoolBullets _ricochetBullets;
        private int _countBulletsInPool = 50; //На данный момент максимальное количество существующих одновлеменно пуль
        private int _countRicochetInPool = 20; //Аналогично для рикошетных пуль

        private void Awake()
        {
            _bullets = new PoolBullets();
            _ricochetBullets = new PoolBullets();
            
            CreateAmmunitions(_bullet, _countBulletsInPool, _bullets);
            CreateAmmunitions(_ricochetBullet, _countRicochetInPool, _ricochetBullets);
            
        }

        private void CreateAmmunitions(Ammunition ammunition, int count, PoolBullets pool)
        {
            for (int i = 0; i < count; i++)
            {
                Ammunition bullet = Instantiate(ammunition);
                bullet.SetActive(false);
               pool.InsertBullets(bullet);
            }
        }

        public Ammunition GetAmmunition<T>(T value, Vector3 position, Quaternion rotation) where T : class
        {
            Ammunition bullet = null;
            if (value.Equals(typeof(Bullet)))
            {
                bullet = _bullets.GetAmmunition();
            }
            if (value.Equals(typeof(RicochetBullet)))
            {
                bullet = _ricochetBullets.GetAmmunition();
            }

            if (bullet != null)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
            return null;
        }

        public void InsertBullet(Ammunition ammunition)
        {
            if (ammunition is Bullet)
            {
                _bullets.InsertBullets(ammunition);
            }
            
            if (ammunition is RicochetBullet)
            {
                _ricochetBullets.InsertBullets(ammunition);
            }
        }
    }
}