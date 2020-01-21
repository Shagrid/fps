using UnityEngine;

namespace Geekbrains
{
    class RicochetBullet : Ammunition
    {
        private int _countRicochet = 1;
        private void OnCollisionEnter(Collision collision)
        {
            // дописать доп урон
            
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(_curDamage, Rigidbody.velocity));
            }
            else
            {
                if (_countRicochet > 0)
                {
                    _curDamage = _curDamage / 2;
                    _countRicochet--;
                    return;
                }
                
            }

            DestroyAmmunition();

        }
    }
}
