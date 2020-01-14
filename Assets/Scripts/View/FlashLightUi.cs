using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class FlashLightUi : MonoBehaviour
    {
        private Text _text;
        [SerializeField]private Image _image;

        private void Awake()
        {
            _text = GetComponent<Text>();
            
        }

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        public void SetBatteryBar(float value)
        {
            _image.fillAmount = value;
        }
    }
}
