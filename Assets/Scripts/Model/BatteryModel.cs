using UnityEngine;

namespace Geekbrains
{
    public sealed class BatteryModel : BaseMakeUsingModel
    {
        private FlashLightModel _flashLight;
        [SerializeField]private float _charge = 10;

        protected override void Awake()
        {
            _flashLight = Object.FindObjectOfType<FlashLightModel>();
        }

        public override void MakeUsing()
        {
            _flashLight.BatteryCharging(_charge);
        }
    }
}
