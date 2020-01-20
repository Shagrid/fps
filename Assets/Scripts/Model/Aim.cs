using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class Aim : MonoBehaviour, ISetDamage, ISelectObj
    {
        public event Action OnPointChange;
		
        public float Hp = 100;
        public float Armor = 100;
        private float _reductionFactor = 4; //Погашение урона на четверть
        private float _reducingArmor = 2; //Броня уменьшается на половину от урона
        private bool _isDead;
        
        //todo дописать поглащение урона
        public void SetDamage(InfoCollision info)
        {
            if (_isDead) return;
            var damage = info.Damage;
            if (Armor > 0)
            {
                Armor -= damage / _reducingArmor;
                if (Armor < 0)
                {
                    Armor = 0;
                }

                damage -= damage / _reductionFactor;
            }
            if (Hp > 0)
            {
                Hp -= damage;
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, 10);

                OnPointChange?.Invoke();
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return gameObject.name;
        }
    }
}
