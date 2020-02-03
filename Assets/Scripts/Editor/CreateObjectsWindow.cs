using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geekbrains.Editor
{
    public class CreateObjectsWindow : EditorWindow
    {
        private GameObject _objectForAddScene;
        private string _nameObject;
        private float _levelWidth;
        private float _levelHeight;
        private Transform _levelGround;
        private int _countObjects;
        private bool _isUniqueName;

        private void OnGUI()
        {
            GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
            _objectForAddScene =
                EditorGUILayout.ObjectField("Объект который хотим вставить", _objectForAddScene, typeof(GameObject),
                        true)
                    as GameObject;
            _levelGround = EditorGUILayout.ObjectField("Объект земли", _levelGround, typeof(Transform),
                true) as Transform;

            _levelWidth = EditorGUILayout.FloatField("Ширина поля", _levelWidth);
            _levelHeight = EditorGUILayout.FloatField("Длина поля", _levelHeight);
            _isUniqueName = EditorGUILayout.Toggle("Уникальные имена", _isUniqueName);
            _countObjects = EditorGUILayout.IntField("Количество объектов", _countObjects);
            
            if (GUILayout.Button("Создать объекты"))
            {
                SpawnObjects(_objectForAddScene, _countObjects);
            }
        }
        
        private void SpawnObjects(GameObject objectToSpawn, int countOfObjects)
        {
            if (objectToSpawn)
            {
                GameObject root = new GameObject(objectToSpawn.name + " Root");
                for (int i = 0; i < countOfObjects; i++)
                {
                    Vector3 randomPoint = Vector3.zero;
                    randomPoint = GenerateRandomPointOnLevel();

                    SpawnObject(objectToSpawn, randomPoint, root);
                }
            }
        }
        
        private Vector3 GenerateRandomPointOnLevel()
        {
            Vector3 randomPoint;
            randomPoint.x = Random.Range(-_levelWidth / 2, _levelWidth / 2);
            randomPoint.y = 50;
            randomPoint.z = Random.Range(-_levelHeight / 2, _levelHeight / 2);

            randomPoint += _levelGround.position;
            return randomPoint;
        }
        
        private void SpawnObject(GameObject objectToSpawn, Vector3 randomPoint, GameObject root)
        {
            RaycastHit hit;
            Physics.Raycast(randomPoint, Vector3.down, out hit, 50);
            if (hit.collider != null)
            {
                Vector3 spawnPosition = hit.point;
                spawnPosition.y += 1.5f;

                GameObject temObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                temObject.transform.SetParent(root.transform);

                if (_isUniqueName)
                {
                    temObject.name = Guid.NewGuid().ToString();
                }
            }
        }
        
    }
}