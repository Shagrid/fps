using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private int _mouseButton = (int)MouseButton.LeftButton;

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
		
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            ScrollingWheelMouse(Input.GetAxis("Mouse ScrollWheel"));

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectWeapon(2);
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<WeaponController>().ReloadClip();
            }
        }

        private void ScrollingWheelMouse(float v)
        {
            if (v == 0) return;
            int weaponIndex = 0;
            if (v > 0)
            {
               weaponIndex =  ServiceLocator.Resolve<Inventory>().NextWeapon();
            }
            if (v < 0)
            {
                weaponIndex = ServiceLocator.Resolve<Inventory>().PreviousWeapon();
            }
  
            SelectWeapon(weaponIndex);
        }


        /// <summary>
        /// Выбор оружия
        /// </summary>
        /// <param name="i">Номер оружия</param>
        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeapon(i); //todo инкапсулировать
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }
    }
}
