using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Vector3 velocity;

    bool doubleJumpAvailable;
    public Animator anim;

    void Start(){
        doubleJumpAvailable = false;
    }


    void Update()
    {
        var isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            Debug.Log("I am grounded");
            doubleJumpAvailable = false;
            if(velocity.y < 0f){
                velocity.y = -1f;
            }

            if (anim.GetBool("IsFalling") == true)
            {
                controller.height = 2.2f;
                anim.SetBool("IsFalling", false);
                anim.SetBool("IsSplatting", true);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Splat"))
                {
                    controller.enabled = false;
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                    {
                        anim.SetBool("IsSplatting", false);
                        anim.SetBool("GettingUp", true);
                    }
                }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Getting Up"))
            {
                if ( anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                                anim.SetBool("GettingUp", false);
                                controller.enabled = true;
                }
            }
        }
        if (velocity.y < -13f)
            {
                anim.SetBool("IsFalling", true);
            }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction == Vector3.zero)
        {
            anim.SetFloat("Blend", 0, 0.1f, Time.deltaTime); // idle animation
        }

        if(direction.magnitude >= 0.1f)
        {
            // animation
            anim.SetFloat("Blend", .11f); // run animation
            // animation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("IsJumping", true);
            Debug.Log("Jump 1");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            doubleJumpAvailable = true;
        }
        else if (Input.GetButtonDown("Jump") && doubleJumpAvailable){
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsDouble", true);
            Debug.Log("Jump 2");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            doubleJumpAvailable = false;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("DoubleJump"))
        {           
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                if (!isGrounded)
                    {
                        anim.SetBool("IsJumping", false);
                        anim.SetBool("IsDouble", false);
                    }
                }
                if (isGrounded)
                {
                    anim.SetBool("IsJumping", false);
                    anim.SetBool("IsDouble", false);
                }
        }
        if (anim.GetBool("IsFalling") == true){
            controller.height = 2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }

}