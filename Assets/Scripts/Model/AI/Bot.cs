using System;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
	public sealed class Bot : BaseObjectScene
	{
		public float Hp = 100;
		public Vision Vision;
		public Weapon Weapon; //todo с разным оружием 
		public Transform Target { get; set; }
		public NavMeshAgent Agent { get; private set; }
		private float _waitTime = 3;
		private StateBot _stateBot;
		private Vector3 _point;
		private float _stoppingDistance = 2.0f;

        public event Action<Bot> OnDieChange;

        private StateBot StateBot
		{
			get => _stateBot;
			set
			{
				_stateBot = value;
				switch (value)
				{
					case StateBot.None:
						Color = Color.white;
						break;
					case StateBot.Patrol:
                        Color = Color.green;
                        break;
					case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
					case StateBot.Detected:
                        Color = Color.red;
                        break;
					case StateBot.Died:
                        Color = Color.gray;
                        break;
					default:
                        Color = Color.white;
                        break;
				}

			}
		}

		protected override void Awake()
		{
			base.Awake();
			Agent = GetComponent<NavMeshAgent>();
		}

		private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        public void Tick()
        {
	        if (StateBot == StateBot.Died) return;

			if (StateBot != StateBot.Detected)
			{
				
				if (!Agent.hasPath)
				{
					if (StateBot != StateBot.Inspection)
					{
						if (StateBot != StateBot.Patrol)
						{
							StateBot = StateBot.Patrol;
							_point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
							Agent.stoppingDistance = 0;
						}
						else
						{
							if (Vector3.Distance(_point, transform.position) <= 1)
							{
								StateBot = StateBot.Inspection;
								Invoke(nameof(ResetStateBot), _waitTime);
							}
						}
					}
				}
				else
				{
					if (StateBot == StateBot.None && Agent.hasPath)
					{
						Agent.ResetPath();
					}
				}

				if (Vision.VisionM(transform, Target))
				{
					StateBot = StateBot.Detected;
					CancelInvoke(nameof(ResetStateBot));
				}
			}
			else
			{
				if (Agent.stoppingDistance != _stoppingDistance)
				{
					Agent.stoppingDistance = _stoppingDistance;
				}
				if (Vision.VisionM(transform, Target))
				{
					Weapon.Fire();
					if (Weapon.Clip.CountAmmunition == 0)
					{
						Weapon.ReloadClip();
					}
				}
				else
				{
					MovePoint(Target.position);
				}

				if (Vision.CheckOnLoss(transform, Target))
				{
					ResetStateBot();
				}
            }
        }

        private void ResetStateBot()
        {
	        StateBot = StateBot.None;
        }

		private void SetDamage(InfoCollision info)
		{
            //todo реакциия на попадание  
            CustumDebug.Log(info.ObjCollision.forward);
			if (Hp > 0)
			{
				Hp -= info.Damage;
				if (StateBot != StateBot.Detected)
				{
					transform.rotation = Quaternion.FromToRotation(Vector3.forward, info.Contact.normal);
					Agent.ResetPath();
					StateBot = StateBot.Inspection;
					Invoke(nameof(ResetStateBot), _waitTime);
				}

				return;
			}

			if (Hp <= 0)
			{
				StateBot = StateBot.Died;
				Agent.enabled = false;
				foreach (var child in GetComponentsInChildren<Transform>())
				{
					child.parent = null;

					var tempRbChild = child.GetComponent<Rigidbody>();
					if (!tempRbChild)
					{
						tempRbChild = child.gameObject.AddComponent<Rigidbody>();
					}
					//tempRbChild.AddForce(info.Dir * Random.Range(10, 300));
					
					Destroy(child.gameObject, 10);
				}

                OnDieChange?.Invoke(this);
            }
		}

		private void MovePoint(Vector3 point)
		{
			Agent.SetDestination(point);
		}

		
	}
}