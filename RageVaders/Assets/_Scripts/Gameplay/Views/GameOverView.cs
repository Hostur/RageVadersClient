using Graphics.MVVM;
using RageVadersData.WebService.HighScores;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming
#pragma warning disable 649

namespace Gameplay.Views
{
	public class GameOverView : View<GameOverViewModel>
	{
		[SerializeField] private Transform _grid;
		[SerializeField] private GameObject _recordPrefab;
		[SerializeField] [ButtonBind("OnCloseButtonClick")] private Button _closeButton;

		[PropertyBind]
		private HighScores _highScores
		{
			get => null;
			set => SpawnHighScoreRecords(value);
		}

		private void SpawnHighScoreRecords(HighScores highScores)
		{
			foreach (HighScoreRecord highScoresRecord in highScores.Records)
			{
				var instance = Instantiate(_recordPrefab);
				instance.GetComponent<UI.HighScoreRecord>().Assign(highScoresRecord);
				instance.transform.SetParent(_grid, false);
			}
		}
	}
}
