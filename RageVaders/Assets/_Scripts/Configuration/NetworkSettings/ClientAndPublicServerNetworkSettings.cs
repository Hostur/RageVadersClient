using RageVadersData;

namespace Configuration.NetworkSettings
{
	public class ClientAndPublicServerNetworkSettings : IRVNetworkSettings
	{
		public int FrameRate { get; set; } = 30;
		public int ClientsCapacity { get; }
		public int ServerAliveEntitiesCapacity { get; }
		public int SingleRequestsQueueCapacity { get; }
		public int DefaultServerPort { get; }
		public int ChunkSize { get; }
		public int BigChunkSize { get; }
		public int MapWidth { get; }
		public int MapHeight { get; }
		public bool ForwardPorts { get; private set; } = true;  // Used by IServerStarter in RVModulesRunner
		public bool PublishServer { get; private set; } = true; // Used by IServerStarter in RVModulesRunner
		public int MillisecondsRequestsDelay { get; }
		public string ServerName { get; } = "Template";
		public bool MockWebService { get; } = true;
		public string WebServiceAddress { get; } = string.Empty;
		public SetupType SetupType => SetupType.ClientAndPublicServer;
	}
}
