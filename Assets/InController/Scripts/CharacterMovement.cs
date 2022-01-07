using UnityEngine;

namespace InController.Scripts
{
    [RequireComponent(typeof(CharacterController2D))]
    public class CharacterMovement : MonoBehaviour
    {
        public CharacterController2D controller;
        private Vector2 motion;
        private bool jumping;
        private bool doubleJump;

        // variable jump
        public float airTimeLimit = 0.3f;
        public float airTime;
    
        private void Update()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            motion = new Vector2
            {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
            if (Input.GetButtonDown("Jump") && controller.IsGrounded)
            {
                jumping = true;
                airTime = 0;
            }

            if (Input.GetButtonDown("Jump") && controller.CanDoubleJump)
            {
                doubleJump = true;
            }
        
            if (jumping)
            {
                airTime += Time.deltaTime;
            }

            // Variable jump height
            if (Input.GetButtonUp("Jump") | airTime > airTimeLimit)
            {
                jumping = false;
                doubleJump = false;
            }

        
#endif
        }

        public void FixedUpdate()
        {
            controller.Move(motion, jumping, doubleJump);
            // LESSON: Alternative to just setting this value is to change it on the state of a keypress: up, down or long press
            jumping = false; // prevents continuous jumping
            // doubleJump = false;
        }
    }
}
