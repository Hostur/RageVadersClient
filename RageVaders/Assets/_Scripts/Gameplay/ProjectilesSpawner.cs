using System;
using System.Collections;
using System.Collections.Generic;
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
		//[SerializeField] private Projectile[] _projectiles;
		private Dictionary<int, Projectile> _projectiles;

		protected override void OnAwake()
		{
			base.OnAwake();
			//_projectiles = new Projectile[_clientShoots.Shoots.Length];
			_projectiles = new Dictionary<int, Projectile>(10);
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
			this.Log("Trying to destroy shoot.");
			DestroyClientShootEvent e = arg as DestroyClientShootEvent;
			Destroy(_projectiles[e.Shoot.UniqueShootId].gameObject);
			//_clientShoots.Shoots[e.Shoot.InternalId] = new RVShoot();
			_projectiles.Remove(e.Shoot.UniqueShootId);
		}


		private void OnProjectileLoaded(RVShoot shoot, GameObject projectile)
		{
			Quaternion rotation = Quaternion.Euler(shoot.InternalId < _networkSettings.ClientsCapacity ? 90 : -90, 0, 0);

			var instance = Instantiate(projectile, shoot.Position.ToUnityVector(), rotation);
			var projectileComponent = instance.GetComponent<Projectile>();
			projectileComponent.Init(shoot.UniqueShootId);
			_projectiles.Add(shoot.UniqueShootId, projectileComponent);
		}
	}
}
