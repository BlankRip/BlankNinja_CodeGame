using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ninja
{
	public class PlayerInputControls : MonoBehaviour
	{
		[HideInInspector] public Vector2 move;

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void MoveInput(Vector2 newMoveInputDirection)
		{
			move = newMoveInputDirection;
		}
	}

}
