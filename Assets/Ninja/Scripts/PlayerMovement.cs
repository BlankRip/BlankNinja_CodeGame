using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ninja
{
	public class PlayerMovement : MonoBehaviour
	{

		private PlayerInputControls _playerInputControls;
		private CharacterController _characterController;

		[SerializeField] private float moveSpeed;

		private float horizontalInput, verticalInput;
		private Vector3 playerDirection;

		private void Awake()
		{
			_playerInputControls = GetComponent<PlayerInputControls>();
			_characterController = GetComponent<CharacterController>();
		}

		private void Update()
		{
			Vector2 moveDirection = _playerInputControls.move;
			horizontalInput = _playerInputControls.move.x;
			verticalInput = _playerInputControls.move.y;

			playerDirection = transform.forward * verticalInput + transform.right * horizontalInput;
			_characterController.Move(playerDirection * moveSpeed * Time.deltaTime);
			
		}

	}
}
