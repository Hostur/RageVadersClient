using Graphics.MVVM;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Gameplay.Views
{
	public class MenuView : View<MenuViewModel>
	{
		[SerializeField] [ButtonBind("OnPlayButtonClick", "CanClickPlayButton")] private Button _playButton;
		[SerializeField] [ButtonBind("OnExitButtonClick")] private Button _exitButton;
		[SerializeField] private AssetReference _gameScene;

		protected override void OnAwake()
		{
			base.OnAwake();
			ViewModel.InitializeSceneToLoad(_gameScene);
		}
	}
}
