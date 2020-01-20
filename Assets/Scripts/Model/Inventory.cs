using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[5];

		public Weapon[] Weapons => _weapons;
		public int CurrentWeapon {get; private set;}

		public FlashLightModel FlashLight { get; private set; }

		public void Initialization()
		{
			_weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}

		public Weapon GetWeapon(int i)
		{
			return Weapons[i];
		}
		//todo Добавить функционал

        public void RemoveWeapon(Weapon weapon)
        {
	        for (int i = 0; i < Weapons.Length; i++)
	        {
		        if (weapon.GetHashCode() == Weapons[i].GetHashCode())
		        {
			        Weapons[i] = null;
			        
			        return;
		        }
	        }
        }

        public void AddWeapon(Weapon weapon)
        {
	        for (int i = 0; i < Weapons.Length; i++)
	        {
		        if (Weapons[i] == null)
		        {
			        Weapons[i] = weapon;
		        }
	        }

	        throw new Exception("Найти время и придумать обработчик на случай, если место не найдено!");
        }

        
	}
}