using System.Globalization;
using TMPro;
using UnityEngine;

#pragma warning disable 649

namespace Gameplay.UI
{
	public class HighScoreRecord : MonoBehaviour
	{
		[SerializeField] private TMP_Text _saveTime;
		[SerializeField] private TMP_Text _playTime;
		[SerializeField] private TMP_Text _killedEnemies;

		public void Assign(RageVadersData.WebService.HighScores.HighScoreRecord recordData)
		{
			_saveTime.text = recordData.UploadTime.ToString(CultureInfo.InvariantCulture);
			_playTime.text = recordData.PlayTime.ToString(@"hh\:mm\:ss");
			_killedEnemies.text = recordData.KilledEntities.ToString();
		}
	}
}
