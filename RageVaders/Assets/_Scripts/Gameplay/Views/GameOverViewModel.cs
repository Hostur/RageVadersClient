using System.Linq;
using System.Threading.Tasks;
using Graphics.MVVM;
using RageVadersData;
using RageVadersData.WebService;
using RageVadersData.WebService.HighScores;

// ReSharper disable InconsistentNaming

namespace Gameplay.Views
{
	[RVRegister(true)]
	public class GameOverViewModel : ViewModelBase
	{
		private readonly IRVWebServiceProvider _webServiceProvider;

		private HighScores _highScores { get; set; }

		public GameOverViewModel(IRVWebServiceProvider webServiceProvider)
		{
			_webServiceProvider = webServiceProvider;
		}

		public override async Task PostInitializeAsync()
		{
			await GetHighScores().ConfigureAwait(true);
		}

		private async Task GetHighScores()
		{
			_highScores = await _webServiceProvider.GetHighScores().ConfigureAwait(true);
			_highScores.Records = _highScores.Records.OrderByDescending(s => s.KilledEntities).Take(10).ToList();
			FireOnPropertyChanged(() => _highScores);
		}

		private void OnCloseButtonClick()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			UnityEngine.Application.Quit();
#endif
		}
	}
}
