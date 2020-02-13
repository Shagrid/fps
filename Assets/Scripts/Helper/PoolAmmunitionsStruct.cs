
using System;
using System.Collections.Generic;

namespace Geekbrains
{
    public sealed class PoolAmmunitionsStruct 
    {
        
        private Queue<Ammunition> _ammunitions = new Queue<Ammunition>();

        public PoolAmmunitionsStruct()
        {
            _ammunitions = new Queue<Ammunition>();
            
        }

        public Ammunition GetAmmunition()
        {
            if (_ammunitions.Count > 0)
            {
                return _ammunitions.Dequeue();
            }
            return null;
        }

        public void InsertBullets(Ammunition ammunition)
        {
            _ammunitions.Enqueue(ammunition);
            
        }
        
    }
}
