using UnityEngine;
#pragma warning disable 649

namespace Gameplay
{
	public class Projectile : RVBehaviour
	{
		[SerializeField] private float _speed;
		[Get] private Transform _transform;
		private Vector3 _destination;

		public void SetDestination(Vector2 destination)
		{
			_destination = destination;
		}

		private void Update()
		{
			_transform.position = Vector3.Lerp(_transform.position, _destination, _speed * Time.deltaTime);
		}
	}
}
