using RageVadersData;
using UnityEngine;

namespace Gameplay.Ship
{
	[RVRegister(true)]
	public class SpawnedGameEntities
	{
		private readonly GameObject[] _objects;

		public SpawnedGameEntities(IRVNetworkSettings settings) =>
			_objects = new GameObject[settings.ServerAliveEntitiesCapacity];

		public GameObject this[int internalId]
		{
			get => _objects[internalId];
			set => _objects[internalId] = value;
		}
	}
}
