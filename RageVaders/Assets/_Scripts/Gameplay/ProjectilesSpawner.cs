using System;
using System.Collections;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine;
using UnityEngine.AddressableAssets;
#pragma warning disable 649

namespace Gameplay
{
	public class ProjectilesSpawner : RVBehaviour
	{
		[RVInject] private RVClientShoots _clientShoots;
		[RVInject] private IRVNetworkSettings _networkSettings;
		[SerializeField] private AssetReference _projectile;
		[SerializeField] private Projectile[] _projectiles;
		

		protected override void OnAwake()
		{
			base.OnAwake();
			_projectiles = new Projectile[_clientShoots.Shoots.Length];
		}

		[RVRegisterEventHandler(typeof(SpawnClientShootEvent))]
		private void OnSpawnClientShootEvent(object sender, EventArgs arg)
		{
			SpawnClientShootEvent e = arg as SpawnClientShootEvent;
			StartCoroutine(this.LoadAsync(_projectile, x => OnProjectileLoaded(e.Shoot, x)));
		}

		[RVRegisterEventHandler(typeof(DestroyClientShootEvent))]
		private void OnDestroyClientShootEvent(object sender, EventArgs arg)
		{
			DestroyClientShootEvent e = arg as DestroyClientShootEvent;
			Destroy(_projectiles[e.Shoot.InternalId].gameObject);
			_projectiles[e.Shoot.InternalId] = null;
		}


		private void OnProjectileLoaded(RVShoot shoot, GameObject projectile)
		{
			Quaternion rotation = Quaternion.Euler(shoot.InternalId < _networkSettings.ClientsCapacity ? 90 : -90, 0, 0);
			var instance = Instantiate(projectile, shoot.Position.ToUnityVector(), rotation);
			_projectiles[shoot.InternalId] = instance.GetComponent<Projectile>();
		}

		private void Update()
		{
			for (int i = 0; i < _clientShoots.Shoots.Length; i++)
			{
				var shoot = _clientShoots.Shoots[i];
				if(shoot.IsEmpty) continue;
				_projectiles[shoot.InternalId]?.SetDestination(shoot.Position + shoot.Velocity);
			}
		}
	}
}
