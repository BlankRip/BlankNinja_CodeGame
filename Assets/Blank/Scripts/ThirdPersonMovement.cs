using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Blank.Gameplay.Player
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [Header("Standard Movement Data")]
        [SerializeField] private float speed = 10.0f;

        [Header("Sprint Data")]
        [SerializeField] private float sprintSpeed = 20.0f;
        [SerializeField] bool useMuliplierForSprint;
        [SerializeField] [Range(1.0f, 4.0f)] float sprintMultiplier = 1.0f;

        [Header("Gravity")]
        [SerializeField] private float gravity = -18.2f;
        [Header("Jump")]
        [SerializeField] private float jumpHight = 5.0f;
        [SerializeField] private int maxJumps = 1;

        private CharacterController cc;
        private Vector3 gravityVelocity;
        private bool grounded;
        private float currentSpeed;
        private int jumpsPerformed;

        private void Start() {
            cc = GetComponent<CharacterController>();
            if(cc == null)
                Debug.LogError("The Charactor Controller is missing");
            if(gravity > 0)
                gravity *= -1.0f;
            if(maxJumps < 1)
                maxJumps = 1;
            SetNewSprintMultiplayer();
            currentSpeed = speed;
        }

        public void HandleSprint(bool sprinting)
        {
            if(sprinting)
                currentSpeed = sprintSpeed;
            else
                currentSpeed = speed;
        }

        public void HandleSprint(InputAction sprintAction)
        {
            if(sprintAction.WasPressedThisFrame())
                currentSpeed = sprintSpeed;
            if(sprintAction.WasReleasedThisFrame())
                currentSpeed = speed;
        }

        public void SetNewSprintMultiplayer()
        {
            if(useMuliplierForSprint)
                sprintSpeed = speed * sprintMultiplier;
        }

        public void HandleMovement(float horizontalInput, float verticalInput)
        {
            Vector3 moveDir =  (transform.right * horizontalInput) + (transform.forward * verticalInput);
            cc.Move(moveDir * currentSpeed * Time.deltaTime);
        }

        public void HandleGravity()
        {
            if(grounded && gravityVelocity.y <= 0.0f)
                gravityVelocity.y = -2.0f;

            gravityVelocity.y += gravity * Time.deltaTime;
            cc.Move(gravityVelocity * Time.deltaTime);
            grounded = cc.isGrounded;
        }

        public void HandleJump(bool jump)
        {
            if(jump)
            {
                if(grounded)
                    gravityVelocity.y = Mathf.Sqrt(-2 * gravity * jumpHight);
            }
        }

        public void HandleMulitpleJumps(bool jump)
        {
            if(grounded)
                jumpsPerformed = 0;

            if(jumpsPerformed < maxJumps && jump)
            {
                gravityVelocity.y = Mathf.Sqrt(-2 * gravity * jumpHight);
                jumpsPerformed++;
            }
        }
    }
}