
using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class MedKit : BaseObjectScene, ISelectObj
    {
        [SerializeField] private string _name = "Аптечка";
        [SerializeField]private float _hpPower = 20;

        public event Action<MedKit> OnHealing;
        
        public void Tick()
        {
            if (Physics.SphereCast(transform.position, 2f, transform.forward, out var hit, 1f))
            {
                var tempObj = hit.transform.root.GetComponent<IHealing>();
                
                if (tempObj != null)
                {
                    if (tempObj.Healing(_hpPower))
                    {
                        OnHealing.Invoke(this);
                        Destroy(gameObject);
                    }
                }
            }
        }
        
        public string GetMessage()
        {
            return _name;
        }
    }
}