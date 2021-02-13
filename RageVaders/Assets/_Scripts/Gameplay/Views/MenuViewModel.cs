using System.Collections;
using Graphics.MVVM;
using RageVadersData;
using UnityEngine.AddressableAssets;

#pragma warning disable 649

namespace Gameplay.Views
{
	[RVRegister(false)]
	public class MenuViewModel : ViewModelBase
	{
		private readonly RVMainThreadActionsQueue _actionsQueue;
		private AssetReference _gameSceneAssetReference;
		private bool _loading = false;

		public MenuViewModel(RVMainThreadActionsQueue actionsQueue)
		{
			_actionsQueue = actionsQueue;
		}

		internal void InitializeSceneToLoad(AssetReference gameSceneAssetReference)
		{
			_gameSceneAssetReference = gameSceneAssetReference;
		}

		private void OnPlayButtonClick()
		{
			_loading = true;
			_actionsQueue.Enqueue(LoadGameScene());
			Refresh();
		}

		private void OnExitButtonClick()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			UnityEngine.Application.Quit();
#endif
		}

		private bool CanClickPlayButton => !_loading;

		public override void Refresh()
		{
			base.Refresh();
			FireOnPropertyChanged(() => CanClickPlayButton);
		}

		private IEnumerator LoadGameScene()
		{
			yield return _gameSceneAssetReference.LoadSceneAsync();
		}
	}
}
