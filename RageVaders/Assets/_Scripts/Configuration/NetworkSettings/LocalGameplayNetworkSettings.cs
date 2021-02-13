using RageVadersData;

namespace Configuration.NetworkSettings
{
	public class LocalGameplayNetworkSettings : IRVNetworkSettings
	{
		public int FrameRate { get; set; } = 30;
		public int ClientsCapacity { get; private set; } = 2;
		public int ServerAliveEntitiesCapacity { get; private set; } = 200;
		public int SingleRequestsQueueCapacity { get; private set; } = 12;
		public int DefaultServerPort { get; private set; } = 6331;
		public int MapWidth { get; private set; } = 60;
		public int MapHeight { get; private set; } = 30;
		public bool ForwardPorts { get; private set; } = false;  // Used by IServerStarter in RVUnityModulesRunner
		public bool PublishServer { get; private set; } = false; // Used by IServerStarter in RVUnityModulesRunner
		public int MillisecondsRequestsDelay { get; private set; } = 0;
		public string ServerName { get; private set; } = "RV Server";
		public bool MockWebService { get; private set; } = true;
		public string WebServiceAddress { get; private set; } = string.Empty;
		public SetupType SetupType => SetupType.LocalGameplay;
	}
}
