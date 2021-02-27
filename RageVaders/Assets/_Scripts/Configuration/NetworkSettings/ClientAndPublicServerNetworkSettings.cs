using RageVadersData;

namespace Configuration.NetworkSettings
{
	public class ClientAndPublicServerNetworkSettings : IRVNetworkSettings
	{
		public int FrameRate { get; set; } = 30;
		public int ClientsCapacity { get; private set; } = 2;
		public int ServerAliveEntitiesCapacity { get; private set; } = 200;
		public int SingleRequestsQueueCapacity { get; private set; } = 12;
		public int DefaultServerPort { get; private set; } = 6331;
		public int MapWidth { get; private set; } = 60;
		public int MapHeight { get; private set; } = 30;
		public bool ForwardPorts { get; private set; } = true;  // Used by IServerStarter in RVUnityModulesRunner
		public bool PublishServer { get; private set; } = true; // Used by IServerStarter in RVUnityModulesRunner
		public int MillisecondsRequestsDelay { get; private set; } = 0;
		public string ServerName { get; private set; } = "RV Server";
		public bool MockWebService { get; private set; } = false;
		public string WebServiceAddress { get; private set; } = string.Empty;
		public SetupType SetupType => SetupType.ClientAndPublicServer;
		public byte PlayerLives { get; private set; } = 3;
		public SerializableVector2 StartingPlayerPosition { get; private set; } = new SerializableVector2(30, 2);
		public float PlayerShipAcceleration { get; private set; } = 10;
		public float PlayerShipMass { get; private set; } = 5;

		public float ShootingCooldown => 5f;
		public float ShootsSpeed => 14;

		public int WaveUpdatesFrequency => 120;
		public byte EntitiesPerWave => 16;
	}
}
