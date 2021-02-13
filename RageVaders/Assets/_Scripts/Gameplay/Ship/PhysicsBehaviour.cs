using RageVadersData;
using RageVadersData.Physics2D.Collections;
using UnityEngine;

namespace Gameplay.Ship
{
	public abstract class PhysicsBehaviour : RVBehaviour
	{
		protected int InternalId = -1;

		[RVInject] protected RV2DVelocities Velocities;
		[RVInject] protected RV2DVelocityDirections VelocityDirections;
		[RVInject] protected RV2DAccelerations Accelerations;
		[RVInject] protected RV2DPositions Positions;
		[RVInject] protected RVInputs Inputs;

		[Get] protected Transform MyTransform;

		protected Vector3 TransformPosition => MyTransform.position;
		protected Vector3 Position => Positions[InternalId].ToUnityVector();
		protected Vector3 Velocity => Velocities[InternalId].ToUnityVector();

		public virtual void Init(int internalId) => InternalId = internalId;
	}
}
