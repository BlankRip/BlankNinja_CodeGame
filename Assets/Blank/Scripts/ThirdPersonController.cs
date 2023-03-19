using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  Blank.Gameplay.Player
{
    public class ThirdPersonController : MonoBehaviour
    {
        [SerializeField] ThirdPersonMovement playerMovement;

        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction sprintAction;

        private void Start() {
            playerInput = Input.GetPlayerInput();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            sprintAction = playerInput.actions["Sprint"];

            if(playerMovement == null)
            {
                playerMovement = GetComponent<ThirdPersonMovement>();
                if(playerMovement == null)
                    Debug.LogError("Plyer Movement Script not assigned");
            }
        }

        private void Update() {
            Vector2 moveData = moveAction.ReadValue<Vector2>();
            playerMovement.HandleSprint(sprintAction);
            playerMovement.HandleMovement(moveData.x, moveData.y);
            playerMovement.HandleGravity();
            playerMovement.HandleMulitpleJumps(jumpAction.WasPressedThisFrame());
        }
    }
}