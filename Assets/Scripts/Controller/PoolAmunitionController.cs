using UnityEngine;
namespace Geekbrains
{
    public sealed class PoolAmunitionController : BaseController
    {
        private PoolBullets _bullets;
        private PoolBullets _ricochetBullets;
        private int _countBulletsInPool = 50; //На данный момент максимальное количество существующих одновлеменно пуль
        private int _countRicochetInPool = 20; //Аналогично для рикошетных пуль
        
        public PoolAmunitionController()
        {
            _bullets = new PoolBullets(AmmunitionType.Bullet, _countBulletsInPool);
            _ricochetBullets = new PoolBullets(AmmunitionType.RicochetBullet, _countRicochetInPool);

        }
    }
}
