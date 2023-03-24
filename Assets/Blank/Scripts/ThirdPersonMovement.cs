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
        [SerializeField] private bool useMuliplierForSprint;
        [SerializeField] [Range(1.0f, 4.0f)] private float sprintMultiplier = 1.0f;
        [SerializeField] private bool useAccleration;
        [SerializeField] private float accleration = 1.0f;
        [SerializeField] private float decleration = 1.0f;

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
        private float targetSpeed;
        private Vector2 moveInput;

        private void Start() {
            cc = GetComponent<CharacterController>();
            if(cc == null)
                Debug.LogError("The Charactor Controller is missing");
            if(gravity > 0)
                gravity *= -1.0f;
            if(maxJumps < 1)
                maxJumps = 1;
            SetNewSprintMultiplayer();
            ChangeCurrentSpeed(ref speed);
        }

        public void HandleSprint(bool keyPressed, bool keyReleased)
        {
            if(keyPressed)
                ChangeCurrentSpeed(ref sprintSpeed);
            if(keyReleased)
                ChangeCurrentSpeed(ref speed);
        }

        public void HandleSprint(InputAction sprintAction)
        {
            if(sprintAction.WasPressedThisFrame())
                ChangeCurrentSpeed(ref sprintSpeed);
            if(sprintAction.WasReleasedThisFrame())
                ChangeCurrentSpeed(ref speed);
        }

        private void ChangeCurrentSpeed(ref float newSpeed)
        {
            if(useAccleration)
                targetSpeed = newSpeed;
            else
                currentSpeed = newSpeed;
        }

        public void SetNewSprintMultiplayer()
        {
            if(useMuliplierForSprint)
                sprintSpeed = speed * sprintMultiplier;
        }

        public void HandleMovement(float horizontalInput, float verticalInput)
        {
            if(horizontalInput == 0 && verticalInput == 0 && currentSpeed > 0.02f)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, decleration * Time.deltaTime);
                horizontalInput = moveInput.x;
                verticalInput = moveInput.y;
            }
            else
                currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, accleration * Time.deltaTime);
            
            Vector3 moveDir =  (transform.right * horizontalInput) + (transform.forward * verticalInput);
            cc.Move(moveDir * currentSpeed * Time.deltaTime);
            moveInput.x = horizontalInput;
            moveInput.y = verticalInput;
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