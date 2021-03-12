using RageVadersData;
using UnityEngine;
#pragma warning disable 649

namespace Gameplay.Ship
{
	public class ShipKeyboardMovementController : PhysicsBehaviour
	{
		private RVInput _input;
		private NetworkRequest _request;
		private Touch _touch;
		[Find] private Camera _camera;
		[RVInject] private RVOutgoingClientRequests _outgoingClientRequests;

		private void Update()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			MobileMovement();
#else
			EditorMovement();
#endif
			PrepareRequest();
			_outgoingClientRequests.EnqueueUdp(ref _request);
		}

		private void PrepareRequest()
		{
			InputRequest inputRequest = new InputRequest(_input);
			_request = new NetworkRequest(InputRequest.REQUEST_IDENTIFIER, inputRequest.Serialize());
		}

		private void EditorMovement()
		{
			bool isLeft = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
			_input.SetLeft(isLeft);
			bool isRight = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
			_input.SetRight(isRight);
			_input.SetShooting(Input.GetKey(KeyCode.Space));
		}

		private void MobileMovement()
		{
			Vector3 position = Vector3.zero;
			if (Input.touchCount > 0)
			{
				_touch = Input.GetTouch(0);
				position = _camera.ScreenToWorldPoint(_touch.position);
			}

			if (position == Vector3.zero)
			{
				_input.SetLeft(false);
				_input.SetRight(false);
			}
			else
			{
				bool isLeft = (Position.x > position.x);
				_input.SetLeft(isLeft);
				_input.SetRight(!isLeft);
			}
			_input.SetShooting(true);
		}
	}
}
