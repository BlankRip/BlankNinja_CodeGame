using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank.Gameplay.Player
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float gravity = -18.2f;
        [SerializeField] private float jumpHight = 5.0f;

        private CharacterController cc;
        private Vector3 gravityVelocity;
        private bool grounded;

        private void Start() {
            cc = GetComponent<CharacterController>();
            if(cc == null)
                Debug.LogError("The Charactor Controller is missing");
            if(gravity > 0)
                gravity *= -1.0f;
        }

        public void HandleMovement(float horizontalInput, float verticalInput)
        {
            //grounded = cc.isGrounded;
            Vector3 moveDir =  (transform.right * horizontalInput) + (transform.forward * verticalInput);
            cc.Move(moveDir * speed * Time.deltaTime);
        }

        public void HandleJump(bool jump)
        {
            if(grounded && gravityVelocity.y <= 0.0f)
                gravityVelocity.y = -2.0f;
            if(jump)
            {
                if(grounded)
                    gravityVelocity.y = Mathf.Sqrt(-2 * gravity * jumpHight);
            }

            gravityVelocity.y += gravity * Time.deltaTime;
            cc.Move(gravityVelocity * Time.deltaTime);
            grounded = cc.isGrounded;
        }
    }
}