﻿using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public abstract class Weapon : BaseObjectScene
	{
		protected PoolAmmunitions _poolAmmunitions;
		
		[SerializeField]private int _countAmmunition = 30;
		private int _countClip = 5;
		public Ammunition Ammunition;
		public Clip Clip;

		public AmmunitionType[] AmmunitionTypes = {AmmunitionType.Bullet};

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 999;
		[SerializeField] protected float _rechergeTime = 0.2f;
		private Queue<Clip> _clips = new Queue<Clip>();

		protected bool _isReady = true;

		private void Start()
		{
			_poolAmmunitions = ServiceLocatorMonoBehaviour.GetService<PoolAmmunitions>();
			
			for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip {CountAmmunition = _countAmmunition});
			}
			
			ReloadClip();
		}

		public abstract void Fire();

		protected void ReadyShoot()
		{
			_isReady = true;
		}

		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

		public void ReloadClip()
		{
			if (CountClip <= 0) return;
			Clip = _clips.Dequeue();
		}

		public int CountClip => _clips.Count;
	}
}