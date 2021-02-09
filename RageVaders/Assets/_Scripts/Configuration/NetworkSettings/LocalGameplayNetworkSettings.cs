using RageVadersData;

namespace Configuration.NetworkSettings
{
	public class LocalGameplayNetworkSettings : IRVNetworkSettings
	{
		public int FrameRate { get; set; } = 30;
		public int ClientsCapacity { get; } = 1000;
		public int ServerAliveEntitiesCapacity { get; } = 1200;
		public int SingleRequestsQueueCapacity { get; } = 12;
		public int DefaultServerPort { get; } = 6331;
		public int ChunkSize { get; } = 20;
		public int BigChunkSize { get; } = 60;
		public int MapWidth { get; } = 12000;
		public int MapHeight { get; } = 10000;
		public bool ForwardPorts { get; private set; } = false;  // Used by IServerStarter in RVModulesRunner
		public bool PublishServer { get; private set; } = true; // Used by IServerStarter in RVModulesRunner
		public int MillisecondsRequestsDelay { get; } = 0;
		public string ServerName { get; } = "Template";
		public bool MockWebService { get; } = true;
		public string WebServiceAddress { get; } = string.Empty;
		public SetupType SetupType { get; } = SetupType.LocalGameplay;
	}
}
