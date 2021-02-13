﻿using System;
using System.Threading.Tasks;
using RageVadersData;
using RageVadersData.Client;
using RageVadersData.WebService;
using RageVadersData.WebService.Responses;

namespace Client
{
#pragma warning disable 649

	public class ModulesRunnerStartedEvent : RVGameEvent
	{

	}

	public class GameConnector : RVBehaviour
	{
		[RVInject] private IRVWebServiceProvider _webServiceProvider;
		[RVInject] private RVClientNetworkData _clientNetworkData;
		[RVInject] private IClientConnector _clientConnector;

		[RVRegisterEventHandler(typeof(ModulesRunnerStartedEvent))]
		private async void OnModulesRunnerStartedEvent(object sender, EventArgs arg)
		{
			this.Log("OnModulesRunnerStartedEvent", LogLevel.EditorInfo);
			await TryToConnect().ConfigureAwait(false);
		}

		private async Task TryToConnect()
		{
			await Authorize().ConfigureAwait(false);
			ServerInfo server = await GetServer().ConfigureAwait(false);
			if (server != null)
			{
				this.Log($"Connecting to server: {server.Address}", LogLevel.EditorInfo);
				_clientConnector.Open(server);
			}
			else
			{
				this.Log("Failed to get server.", LogLevel.Error);
			}
		}

		private async Task Authorize()
		{
			_clientNetworkData.AuthorizationToken = await _webServiceProvider.Authorize("abc", "def").ConfigureAwait(false);
		}

		private async Task<ServerInfo> GetServer()
		{
			var servers = await _webServiceProvider.GetServers().ConfigureAwait(false);
			if (servers?.Servers?.Count > 0)
			{
				return servers.Servers[0];
			}

			return null;
		}
	}
}