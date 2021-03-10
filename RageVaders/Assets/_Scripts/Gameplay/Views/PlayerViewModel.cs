using System;
using Graphics.MVVM;
using RageVadersData;
using RageVadersData.Client;

namespace Gameplay.Views
{
	[RVRegister(false)]
	public class PlayerViewModel : ViewModelBase
	{
		private readonly RVClientPlayersData _playersData;
		private readonly RVClientNetworkData _clientNetworkData;

		public PlayerViewModel(RVClientPlayersData playersData, RVClientNetworkData clientNetworkData)
		{
			_playersData = playersData;
			_clientNetworkData = clientNetworkData;
		}

		private PlayerData MyPlayerData => _playersData[_clientNetworkData.MyId];
		private string _ourPlayerPoints { get; set; }
		private string _ourPlayerLives { get; set; }
		
		private bool _died { get; set; }

		public override void Refresh()
		{
			base.Refresh();
			_ourPlayerPoints = MyPlayerData.KilledEntities.ToString("000000");
			_ourPlayerLives = MyPlayerData.Lives.ToString();

			FireOnPropertyChanged(() => _ourPlayerPoints);
			FireOnPropertyChanged(() => _ourPlayerLives);
		}

		[RVRegisterEventHandler(typeof(PlayerDiedEvent))]
		private void OnPlayerDiedEvent(object sender, EventArgs arg)
		{
			_died = _playersData[_clientNetworkData.MyId].Lives < 1;
			FireOnPropertyChanged(() => _died);
		}
	}
}
