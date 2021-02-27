﻿using System;
using System.Linq;
using Gameplay.Ship;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine;
using UnityEngine.AddressableAssets;
#pragma warning disable 649

namespace Gameplay
{
	[Serializable]
	public class HostileShipAssetReference
	{
		public WaveVisualStyle WaveVisualStyle;
		public AssetReference AssetReference;
	}
	public class GameSceneSpawner : RVBehaviour
	{
		[RVInject] private RVClientNetworkData _clientNetworkData;
		[RVInject] private RV2DPositions _positions;
		[RVInject] private SpawnedGameEntities _spawnedGameEntities;

		[SerializeField] private AssetReference _playerAssetReference;
		[SerializeField] private HostileShipAssetReference[] _hostileShips;

		[RVRegisterEventHandler(typeof(ClientConfirmationSuccessEvent))]
		private async void OnClientConfirmationSuccessEvent(object sender, EventArgs arg)
		{
			this.Log("OnClientConfirmationSuccessEvent");
			StartCoroutine(this.LoadAsync(_playerAssetReference, OnClientAssetLoaded));
		}

		private void OnClientAssetLoaded(GameObject obj)
		{
			SerializableVector2 serializablePosition = _positions[_clientNetworkData.MyId];
			Vector3 position = serializablePosition.ToUnityVector();

			var instance = Instantiate(obj, position, Quaternion.Euler(90, 180, 0));
			instance.GetComponent<PhysicsBehaviour>().Init(_clientNetworkData.MyId);
			_spawnedGameEntities[_clientNetworkData.MyId] = instance.gameObject;
		}

		[RVRegisterEventHandler(typeof(WaveSpawnedRequestEvent))]
		private void OnWaveSpawnedRequestEvent(object sender, EventArgs arg)
		{
			this.Log("OnWaveSpawnedRequestEvent");
			WaveSpawnedRequestEvent e = arg as WaveSpawnedRequestEvent;
			AssetReference reference = _hostileShips.FirstOrDefault(s => s.WaveVisualStyle == e.Request.WaveVisualStyle).AssetReference;

			StartCoroutine(this.LoadAsync(reference, x => OnHostileShopAssetLoaded(e.Request.WaveVisualStyle, e.Request.Ships, x)));
		}

		private void OnHostileShopAssetLoaded(WaveVisualStyle visualStyle, RVHostileShip[] hostileShips, GameObject obj)
		{
			for (int i = 0; i < hostileShips.Length; i++)
			{
				var entity = hostileShips[i];
				SerializableVector2 serializablePosition = entity.Position;

				Quaternion rotation = Quaternion.identity;
				switch (visualStyle)
				{
					case WaveVisualStyle.C:
						rotation = Quaternion.Euler(90, 0, 0);
						break;
					case WaveVisualStyle.B:
						rotation = Quaternion.Euler(-90, 0, 0);
						break;
				}
				



				var instance = Instantiate(obj, serializablePosition.ToUnityVector(), rotation);
				instance.GetComponent<PhysicsBehaviour>().Init(entity.InternalId);
				_spawnedGameEntities[entity.InternalId] = instance.gameObject;
			}
		}
	}
}
