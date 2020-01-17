using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _using = KeyCode.E;
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }

            if (Input.GetKeyDown(_using))
            {
                ServiceLocator.Resolve<SelectionController>().makeUse();
            }
        }
    }
}
