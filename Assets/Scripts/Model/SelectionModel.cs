using UnityEngine;

namespace Geekbrains
{
    public sealed class SelectionModel :BaseObjectScene
    {
        private float _distance = 5;
        RaycastHit _hit;

        public string ObjectName { get; private set; }

        public void Switch(bool value)
        {
            if (!value) return;
        }

        public GameObject FindObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out _hit, _distance))
                return _hit.collider.gameObject;

            return null;

        }
    }
}
