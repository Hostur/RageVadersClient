using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration.NetworkSettings;
using RageVadersData;
using RageVadersModules;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

#pragma warning disable 649

[Serializable]
public class SetupConfiguration
{
	public SetupType SetupType;

	public IRVNetworkSettings NetworkSettings
	{
		get
		{
			switch (SetupType)
			{
				case SetupType.LocalGameplay: return new LocalGameplayNetworkSettings();
				case SetupType.StandaloneClient: return new StandaloneClientNetworkSettings();
				case SetupType.ClientAndPublicServer: return new ClientAndPublicServerNetworkSettings();
				case SetupType.LocalHostWithGameplay: return new LocalHostWithGameplayNetworkSettings();
				case SetupType.StandaloneServer: return new StandaloneServerNetworkSettings();
				default: throw new Exception("Standalone server setup not supported.");
			}
		}
	}
}

public class Initialization : RVBehaviour
{
	[SerializeField] private SetupConfiguration _setup;
	[SerializeField] private AssetLabelReference[] _preloadAssetLabels;
	[SerializeField] private AssetReference _mainMenuScene;

	protected override async void OnAwake()
	{
		base.OnAwake();
		OnStart();
		await PreLoadAssets().ConfigureAwait(true);
		StartCoroutine(Finalize());
	}

	private void OnStart()
	{
		Initialize(_setup.SetupType.GetConfiguration(), _setup.NetworkSettings);
	}

	private void Initialize(IRVModulesConfiguration modulesConfiguration, IRVNetworkSettings networkSettings)
	{
		God.WorldCreation(
			new RVUnityLogger(),
			new RVClientAssemblyDefinition(modulesConfiguration, networkSettings),
			new RVDataAssemblyDefinition(networkSettings),
			new RVModuleAssemblyDefinition(networkSettings));
		RVLogger.InitLogger(God.PrayFor<IRVLogger>());


		this.Log("Initialized", LogLevel.EditorInfo);
	}

	private async Task PreLoadAssets()
	{
		foreach (AssetLabelReference assetLabelReference in _preloadAssetLabels)
		{
			await assetLabelReference.PreLoad<Object>().ConfigureAwait(true);
		}
	}

	private IEnumerator Finalize()
	{
		yield return _mainMenuScene.LoadSceneAsync(LoadSceneMode.Single);
	}
}
