using RageVadersData;
using RageVadersData.Physics2D.Collections;

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

		public virtual void Init(int internalId) => InternalId = internalId;
	}
}
