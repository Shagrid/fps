using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class Shotgun : Weapon
    {
        private int _countPelletss = 9;
        private float _spread = 0.05f;
        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            Ammunition ammunition;
            Vector3 orientation;
            for (int i = 0; i < _countPelletss; i++)
            {
                orientation = new Vector3(_barrel.forward.x + Random.Range(-_spread, _spread), 
                                          _barrel.forward.y + Random.Range(-_spread, _spread), 
                                          _barrel.forward.z + Random.Range(-_spread, _spread));
                ammunition = _poolAmmunitions.GetAmmunition(Ammunition.GetType(), _barrel.position, _barrel.rotation);
                ammunition.EnableRigidBody();
                ammunition.AddForce(orientation * _force);
            }
            Clip.CountAmmunition--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechergeTime);
        }
    }
}