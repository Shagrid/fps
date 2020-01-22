
using System.Collections.Generic;

namespace Geekbrains
{
    public sealed class PoolBullets : BaseObjectScene
    {
        
        private Queue<Ammunition> _ammunitions = new Queue<Ammunition>();

        public PoolBullets(AmmunitionType type, int countBullet)
        {
            _ammunitions = new Queue<Ammunition>();
            switch (type)
            {
                case AmmunitionType.None:
                    break;
                case AmmunitionType.Rpg:
                    break;
                case AmmunitionType.Bullet: 
                    InsertBullets(countBullet);
                    break;
                case AmmunitionType.RicochetBullet:
                    InsertRicochetBullets(countBullet);
                    break;
                default:
                    break;
            }
        }

        public Ammunition GetAmmunition()
        {
            if (_ammunitions.Count > 0)
            {
                return _ammunitions.Dequeue();
            }
            return null;
        }

        private void InsertBullets(int counter)
        {
            for (int i = 0; i < counter; i++)
            {
                Bullet bullet = new Bullet();
                bullet.SetActive(false);
                _ammunitions.Enqueue(bullet);
            }
        }
        private void InsertRicochetBullets(int counter)
        {
            for (int i = 0; i < counter; i++)
            {
                RicochetBullet bullet = new RicochetBullet();
                bullet.SetActive(false);
                _ammunitions.Enqueue(bullet);
            }
        }
    }
}
