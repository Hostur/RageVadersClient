using RageVadersData;
using RageVadersData.Client;

namespace Client
{
	public class ClientDisconnectionHandlerEvent : RVGameEvent
	{
		public ClientDisconnectionReason Reason { get; }
		public ClientDisconnectionHandlerEvent(ClientDisconnectionReason reason) => Reason = reason;
	}

	[RVRegister(true)]
	public class ClientDisconnectionHandler : IClientDisconnectionHandler
	{
		public void OnClientDisconnected(ClientDisconnectionReason reason)
		{
			RVMainThreadActionsQueue actionQueue = God.PrayFor<RVMainThreadActionsQueue>();
			actionQueue.Enqueue(() => RVGameEventsManager.Publish(this, new ClientDisconnectionHandlerEvent(reason)));

			this.Log(reason.ToString(), LogLevel.Error);
		}
	}
}