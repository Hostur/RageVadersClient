using System;
using System.Collections.Generic;
using Gameplay;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine.AddressableAssets;

public class ProjectilesSpawner : RVBehaviour
{
	[RVInject] private RVClientShoots _clientShoots;
	private AssetReference _projectile;
	private List<Projectile> _projectiles;

	protected override void OnAwake()
	{
		base.OnAwake();
		_projectiles = new List<Projectile>(24);
	}

	[RVRegisterEventHandler(typeof(SpawnClientShootEvent))]
	private void OnSpawnClientShootEvent(object sender, EventArgs arg)
	{
		SpawnClientShootEvent e = arg as SpawnClientShootEvent;
	}

	[RVRegisterEventHandler(typeof(DestroyClientShootEvent))]
	private void OnDestroyClientShootEvent(object sender, EventArgs arg)
	{
		DestroyClientShootEvent e = arg as DestroyClientShootEvent;
	}
}
