using System;
using Gameplay.Ship;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
	public class GameSceneSpawner : RVBehaviour
	{
		[RVInject] private RVClientNetworkData _clientNetworkData;
		
		[SerializeField] private AssetReference _playerAssetReference;

		[RVRegisterEventHandler(typeof(ClientConfirmationSuccessEvent))]
		private async void OnClientConfirmationSuccessEvent(object sender, EventArgs arg)
		{
			this.Log("OnClientConfirmationSuccessEvent");

			var player = await this.Instantiate<PhysicsBehaviour>(_playerAssetReference, ).ConfigureAwait(true);
			Addressables.InstantiateAsync(_playerAssetReference)
		}
	}
}
