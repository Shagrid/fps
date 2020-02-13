
using UnityEngine;

namespace Geekbrains
{
    public sealed class MedKit : BaseObjectScene
    {
        [SerializeField]private float _hpPower = 20;

        public void Update()
        {
            if (Physics.SphereCast(transform.position, 2f, transform.forward, out var hit, 2f))
            {
                var tempObj = hit.transform.root.GetComponent<IHealing>();
                
                if (tempObj != null)
                {
                    if (tempObj.Healing(_hpPower))
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
        // private void OnCollisionEnter(Collision collision)
        // {
        //     var tempObj = collision.gameObject.GetComponent<IHealing>();
        //     
        //     if (tempObj != null)
        //     {
        //         if (tempObj.Healing(_hpPower))
        //         {
        //             Destroy(gameObject);
        //         }
        //     }
        // }
    }
}