using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    
    float horizontalMove = 0;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        /*
         * The jump animation only works with the 'Has exit time' checked on both transitions
         * OR changed CharacterController2D.cs on line 130 
         * // Add a vertical force to the player. from false to true
		 *	m_Grounded = false;
		 *
		 * // best solution, change this radius
         * OR const float k_GroundedRadius = .2f; changed to 0.05f - line 
         * 
         * Solution: Decrease the ground check radius and check if it works properly 
         * (if it still doesn't work decrease it even more). 
         * This also might not work if you have kept the game object that checks if touching ground too much below the player.
           Explanation: The reason why this is not working is that the program is checking for ground
           at that very split second of the jump since the program is updating too fast the animation check option
           keeps returning to false. If you decrease the radius of the ground check then by the time the program checks 
           for ground the overlapping circle would have moved away from the ground due to low radius thus not 
           overlapping anymore.
           Don't add Exit time I know it works but the problem with that is it only works for fully flat ground, suppose you are jumping to a level higher than you then the sprite would be touching ground before the exit time is over thus remaining in jump animation for more time in spite of touching ground
         */
        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    public void onLanding() {
        animator.SetBool("isJumping", false);
    }
    public void onCrouching(bool isCrouching) {
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate() {
       // Debug.Log("horizontalMove: "+ horizontalMove);

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
