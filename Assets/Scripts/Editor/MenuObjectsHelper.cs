using UnityEditor;

namespace Geekbrains.Editor
{
    
    public class MenuObjectsHelper : UnityEditor.Editor
    {
        [MenuItem("GameDesignerHelper/Add Objects To Scene")]
        public static void CreateObjectsWindow()
        {
            EditorWindow.GetWindow(typeof(CreateObjectsWindow), false, "Add objects to scene");
        }
    }
}