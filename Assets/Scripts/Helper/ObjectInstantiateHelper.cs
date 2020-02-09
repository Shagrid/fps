using System;
using UnityEngine;

namespace Geekbrains
{
    public class ObjectInstantiateHelper : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private string _rootObjectName = "NewObject";
        private GameObject _rootObject;
        
        
        public void InstantiateObj(Vector3 pos)
        {
            if (!_rootObject)
            {
               _rootObject = new GameObject(_rootObjectName);
            }

            if (_prefab != null)
            {
                Instantiate(_prefab, pos, Quaternion.identity, _rootObject.transform);
            }
            else
            {
                throw new Exception($"Нет префаба на {gameObject.name}");
            }
        }

    }
}