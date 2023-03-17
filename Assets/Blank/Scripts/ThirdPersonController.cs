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

        private void Start() {
            playerInput = Input.GetPlayerInput();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];

            if(playerMovement == null)
            {
                playerMovement = GetComponent<ThirdPersonMovement>();
                if(playerMovement == null)
                    Debug.LogError("Plyer Movement Script not assigned");
            }
        }

        private void Update() {
            Vector2 moveData = moveAction.ReadValue<Vector2>();
            playerMovement.HandleMovement(moveData.x, moveData.y);
            playerMovement.HandleJump(jumpAction.WasPressedThisFrame());
        }
    }
}