using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank.Gameplay.Player
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        private CharacterController cc;

        private void Start() {
            cc = GetComponent<CharacterController>();
            if(cc == null)
                Debug.LogError("The Charactor Controller is missing");
        }

        public void HandleMovement(float horizontalInput, float verticalInput)
        {
            Vector3 moveDir =  (transform.right * horizontalInput) + (transform.forward * verticalInput);
            cc.Move(moveDir * speed * Time.deltaTime);
        }
    }
}