using System;
using System.Collections;
using Gameplay.Ship;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine;
using UnityEngine.AddressableAssets;
#pragma warning disable 649

namespace Gameplay
{
	public class GameSceneSpawner : RVBehaviour
	{
		[RVInject] private RVClientNetworkData _clientNetworkData;
		[RVInject] private RV2DPositions _positions;
		[RVInject] private SpawnedGameEntities _spawnedGameEntities;

		[SerializeField] private AssetReference _playerAssetReference;

		[RVRegisterEventHandler(typeof(ClientConfirmationSuccessEvent))]
		private async void OnClientConfirmationSuccessEvent(object sender, EventArgs arg)
		{
			this.Log("OnClientConfirmationSuccessEvent");

			SerializableVector2 serializablePosition = _positions[_clientNetworkData.MyId];
			Vector3 position = serializablePosition.ToUnityVector();


			StartCoroutine(this.LoadAsync<PhysicsBehaviour>(_playerAssetReference, OnAssetLoaded));
		}

		private void OnAssetLoaded(PhysicsBehaviour behaviour)
		{
			SerializableVector2 serializablePosition = _positions[_clientNetworkData.MyId];
			Vector3 position = serializablePosition.ToUnityVector();

			var instance = Instantiate(behaviour, position, Quaternion.identity);
			instance.Init(_clientNetworkData.MyId);
			_spawnedGameEntities[_clientNetworkData.MyId] = instance.gameObject;
		}
	}
}
