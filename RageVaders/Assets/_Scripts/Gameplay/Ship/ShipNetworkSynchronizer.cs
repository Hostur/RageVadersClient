using UnityEngine;

namespace Gameplay.Ship
{
	public class ShipNetworkSynchronizer : PhysicsBehaviour
	{
		[SerializeField] [Tooltip("Max distance from current network position until we move object to its destination without lerp.")] private float _forceSyncPositionThreshold;
		[SerializeField] [Tooltip("Max distance which in we lerp towards velocity. Above that we lerp toward network position.")] private float _maxVelocityLerp;

		[SerializeField] private float _lerpSpeed;


		private void Update()
		{
			Vector3 position = Position;
			Vector3 transformPosition = TransformPosition;
			float distance = Vector3.Distance(transformPosition, position);

			if (distance >= _forceSyncPositionThreshold)
			{
				MyTransform.position = Position;
			}
			else if (distance >= _maxVelocityLerp)
			{
				// Lerp from transform position towards velocity.
				MyTransform.position = Vector3.Lerp(transformPosition, transformPosition + Velocity, _lerpSpeed);
			}
			else
			{
				MyTransform.position = Vector3.Lerp(transformPosition, Position, _lerpSpeed);
			}
		}
	}
}
