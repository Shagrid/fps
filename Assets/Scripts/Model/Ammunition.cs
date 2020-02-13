using UnityEngine;

namespace Geekbrains
{
    public abstract class Ammunition : BaseObjectScene
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;


        public AmmunitionType Type = AmmunitionType.Bullet;
        
        public virtual void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            _curDamage = _baseDamage;
            Rigidbody.AddForce(dir);
            DestroyAmmunition(_timeToDestruct);
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition(float timeToDestruct = 0)
        {
            //Destroy(gameObject, timeToDestruct);
            Invoke(nameof(returnAmmunition), timeToDestruct);
            CancelInvoke(nameof(LossOfDamage));
         
        }

        private void returnAmmunition()
        {
            ServiceLocator.Resolve<PoolAmmunitions>().InsertBullet(this);
            SetActive(false);
            DisableRigidBody();
        }
    }
}
