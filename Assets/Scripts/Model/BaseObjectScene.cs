using UnityEngine;

namespace Geekbrains
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        private int _layer;
        private bool _isVisible;
        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }

        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0)return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                var renderer = GetComponent<Renderer>();
                if (renderer)
                    renderer.enabled = _isVisible;
                AskVisible(Transform, value);
            }
        }

        private void AskVisible(Transform obj, bool value)
        {
            if (Transform.childCount <= 0) return;
            foreach (Transform t in transform)
            {
                var renderer = t.gameObject.GetComponent<Renderer>();
                if (renderer)
                    renderer.enabled = _isVisible;
            }
        }


        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }
    }
}
