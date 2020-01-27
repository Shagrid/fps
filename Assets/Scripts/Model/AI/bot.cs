using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    public sealed class bot : BaseObjectScene
    {
        public float Hp = 100;
        
        public Weapon[] Weapons;
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        private float _whaitTime = 3;
        
        

    }
}