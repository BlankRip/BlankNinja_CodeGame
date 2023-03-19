using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ninja
{
	public class PlayerInputControls : MonoBehaviour
	{
		[HideInInspector] public Vector2 move;
		[HideInInspector] public bool jump;
		[HideInInspector] public bool sprint;

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}
		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void MoveInput(Vector2 newMoveInputDirection)
		{
			move = newMoveInputDirection;
		}
		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
	}

}
