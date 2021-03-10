using System;
using System.Collections;
using RageVadersData;
using RageVadersData.Client;
using UnityEngine;

namespace Gameplay.Ship
{
	public class PlayerShip : RVBehaviour
	{
		[Get] private MaterialColorChanger _materialColorChanger;
		//[RVInject] private IRVNetworkSettings _networkSettings;
		//[RVInject] private RVClientNetworkData _clientNetworkData;
		//[RVInject] private RVClientPlayersData _playersData;
		private Color32 _color32 = new Color32(255, 0, 190, 255);

		[RVRegisterEventHandler(typeof(PlayerDiedEvent))]
		private void OnPlayerDiedEvent(object sender, EventArgs arg)
		{
			this.Log("OnPlayerDiedEvent");
			StopAllCoroutines();
			StartCoroutine(ChangeToDeadColor());
		}

		private IEnumerator ChangeToDeadColor()
		{
			for (byte i = 0; i < 191 / 5; i++)
			{
				_color32.b = (byte)(190 - i * 5);
				_materialColorChanger.SetColor(in _color32);
				yield return null;
			}

			yield return ChangeToAliveColor();
		}

		private IEnumerator ChangeToAliveColor()
		{
			for (byte i = 0; i < 191 / 5; i++)
			{
				_color32.b = (byte)(i * 5);
				_materialColorChanger.SetColor(in _color32);
				yield return null;
			}
		}
	}
}
