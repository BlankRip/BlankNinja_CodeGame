using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ninja
{
	public class PlayerMovement : MonoBehaviour
	{

		private PlayerInputControls _playerInputControls;
		private PlayerInput _input;
		private CharacterController _characterController;

		private InputAction jumpAction;
		private InputAction sprintAction;
		
		private float currentSpeed;

		[SerializeField] private float moveSpeed;
		[SerializeField] private float sprintSpeed;
		[SerializeField] private float gravity;
		[SerializeField] private float jumpHeight;

		private bool isGrounded;
		private float horizontalInput, verticalInput;
		private Vector3 playerDirection;
		private Vector3 gravityVector;

		private void Start()
		{
			_playerInputControls = GetComponent<PlayerInputControls>();
			_characterController = GetComponent<CharacterController>();

			_input = GetComponent<PlayerInput>();

			jumpAction = _input.actions["Jump"];
			sprintAction = _input.actions["Sprint"];

			if(gravity > 0)
			{
				gravity *= -1f;
			}
		}

		private void Update()
		{
			groundCheck();
			PlayerMove();
			PlayerSprint();
			Jump();
			
		}

		public void groundCheck()
		{
			isGrounded = _characterController.isGrounded;
			if(isGrounded && gravityVector.y < 0)
			{
				gravityVector.y = 0;
			}
		}

		public void PlayerMove()
		{
			Vector2 moveDirection = _playerInputControls.move;
			horizontalInput = moveDirection.x;
			verticalInput = moveDirection.y;

			playerDirection = transform.forward * verticalInput + transform.right * horizontalInput;

			_characterController.Move(playerDirection * currentSpeed * Time.deltaTime);
		}

		public void PlayerSprint()
		{
			if(sprintAction.WasPressedThisFrame())
			{
				currentSpeed  = sprintSpeed;
			}
			else if(sprintAction.WasReleasedThisFrame())
			{
				currentSpeed = moveSpeed;
			}
		}

		public void Jump()
		{
			if(jumpAction.WasPressedThisFrame() && isGrounded)
			{
				gravityVector.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
			}
			gravityVector.y += gravity * Time.deltaTime;
			_characterController.Move(gravityVector * Time.deltaTime);
		}
	}
}
