using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class MedKitController : BaseController, IInitialization, IExecute
    {
        private readonly int _countMedKit = 5;
        private readonly HashSet<MedKit> _getMedKits  = new HashSet<MedKit>();
        
        public void Initialization()
        {
            var medKit = Resources.Load<MedKit>(ResourceManager.MEDKIT);
            for (var index = 0; index < _countMedKit; index++)
            {
                var tempMedKit = Object.Instantiate(medKit,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);
                
                AddMedkitToList(tempMedKit);
            }
        }

        private void AddMedkitToList(MedKit medKit)
        {
            if (!_getMedKits.Contains(medKit))
            {
                _getMedKits.Add(medKit);
                medKit.OnHealing += RemoveMedKitToList;
            }
        }

        private void RemoveMedKitToList(MedKit medKit)
        {
            if (!_getMedKits.Contains(medKit))
            {
                return;
            }

            medKit.OnHealing -= RemoveMedKitToList;
            _getMedKits.Remove(medKit);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _getMedKits.Count; i++)
            {
                var medKit = _getMedKits.ElementAt(i);
                medKit.Tick();
            }
        }
    }
}