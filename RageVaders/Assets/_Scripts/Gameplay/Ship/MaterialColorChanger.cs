using UnityEngine;
#pragma warning disable 649

namespace Gameplay.Ship
{
	public class MaterialColorChanger : RVBehaviour
	{
		[Get] private Renderer _renderer;

		public void SetColor(in Color32 color)
		{
			_renderer.material.SetColor("_Color", color);
		}
	}
}
