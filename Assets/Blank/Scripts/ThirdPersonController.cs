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

        private void Start() {
            playerInput = Input.GetPlayerInput();

            if(playerMovement == null)
            {
                playerMovement = GetComponent<ThirdPersonMovement>();
                if(playerMovement == null)
                    Debug.LogError("Plyer Movement Script not assigned");
            }
        }

        private void Update() {
            
        }
    }
}