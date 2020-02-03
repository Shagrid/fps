using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Geekbrains.Editor
{
    [CustomEditor(typeof(ObjectInstantiateHelper))]
    public class ObjectInstantiateHelperEditor : UnityEditor.Editor
    {
        private ObjectInstantiateHelper _testTarget;

        private void OnEnable()
        {
            _testTarget = (ObjectInstantiateHelper)target;
        }


        private void OnSceneGUI()
        {
            if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
            {
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
                    SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

                if (Physics.Raycast(ray, out var hit))
                {
                    _testTarget.InstantiateObj(hit.point);
                    SetObjectDirty(_testTarget.gameObject);
                }
            }

            Selection.activeGameObject = FindObjectOfType<ObjectInstantiateHelper>().gameObject;

        }

        public void SetObjectDirty(GameObject obj)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }
        }

    }
}