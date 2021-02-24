using RageVadersData;
using UnityEngine;
#pragma warning disable 649

namespace Gameplay.Ship
{
	public class ShipKeyboardMovementController : PhysicsBehaviour
	{
		private RVInput _input;
		private NetworkRequest _request;
		[RVInject] private RVOutgoingClientRequests _outgoingClientRequests;

		private void Update()
		{
			bool isLeft = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
			_input.SetLeft(isLeft);
			bool isRight = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
			_input.SetRight(isRight);
			_input.SetShooting(Input.GetKey(KeyCode.Space));

			PrepareRequest();


			_outgoingClientRequests.EnqueueUdp(ref _request);
		}

		private void PrepareRequest()
		{
			InputRequest inputRequest = new InputRequest(_input);
			_request = new NetworkRequest(InputRequest.REQUEST_IDENTIFIER, inputRequest.Serialize());
		}
	}
}
