using Graphics.MVVM;
using RageVadersData;
using TMPro;
using UnityEngine;

namespace Gameplay.Views
{
	public class PlayerView : View<PlayerViewModel>
	{
		[SerializeField] private GameObject _gameOverPanel;
		[SerializeField] [TextBind] private TMP_Text _ourPlayerPoints;
		[SerializeField] [TextBind] private TMP_Text _ourPlayerLives;
		[PropertyBind] private bool _died
		{
			get => false;
			set 
			{
				if (!value)
				{
					this.Log("Show lose 1 life indicator", LogLevel.ToDo);
				}
				else
				{
					ShowEndOfTheGame();
				}
			}
	}

		protected override void OnAwake()
		{
			base.OnAwake();
			InvokeRepeating(nameof(Refresh), 1, 0.5f);
		}

		private void Refresh() => ViewModel.Refresh();

		private void ShowEndOfTheGame()
		{
			_gameOverPanel.SetActive(true);
		}
	}
}
