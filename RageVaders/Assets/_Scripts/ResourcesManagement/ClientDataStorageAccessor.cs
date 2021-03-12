using System.IO;
using RageVadersData;
using UnityEngine;

namespace ResourcesManagement
{
	[RVRegister(true)]
	public class ClientDataStorageAccessor : IDataStorageAccessor
	{
		private readonly IRVNetworkSettings _networkSettings;
		private string _storagePath = null;

		public ClientDataStorageAccessor(IRVNetworkSettings networkSettings)
		{
			_networkSettings = networkSettings;
		}

		public string GetBaseStoragePath() => _storagePath ?? (_storagePath = GetBaseStoragePathInternal());

		private string GetBaseStoragePathInternal()
		{
			return Path.Combine(Path.Combine(Application.persistentDataPath, "RageQuitGames"), _networkSettings.ServerName);
		}
	}
}
