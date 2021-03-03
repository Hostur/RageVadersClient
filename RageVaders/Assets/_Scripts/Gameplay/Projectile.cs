using RageVadersData;
using UnityEngine;
#pragma warning disable 649

namespace Gameplay
{
	public class Projectile : RVBehaviour
	{
		private int _uniqueId;
		[RVInject] private RVClientShoots _shoots;
		[SerializeField] private float _speed;
		[Get] private Transform _transform;

		private Vector3 _destination;

		public void Init(int uniqueId) => _uniqueId = uniqueId;

		private void SetDestination()
		{
			if (_shoots.Shoots.ContainsKey(_uniqueId))
			{
				_destination = _shoots.Shoots[_uniqueId].Position.ToUnityVector();
			}
		}

		private void Update()
		{
			SetDestination();
			_transform.position = Vector3.Lerp(_transform.position, _destination, _speed * Time.deltaTime);
		}
	}
}
