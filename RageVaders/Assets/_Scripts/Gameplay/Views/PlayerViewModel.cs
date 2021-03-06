using Graphics.MVVM;
using RageVadersData;

namespace Gameplay.Views
{
	[RVRegister(false)]
	public class PlayerViewModel : ViewModelBase
	{
		private readonly RVPlayersData _playersData;

		public PlayerViewModel(RVPlayersData playersData)
		{
			_playersData = playersData;
		}
	}
}
