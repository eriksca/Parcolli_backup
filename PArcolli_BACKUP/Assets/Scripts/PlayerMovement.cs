using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
   

    public CharacterController controller;
    public Transform groundCheck;

    [SerializeField] Animator playerAnimator;

    // parametri per il character controller
    private float gravity = -20f;
    private float groundDistance = 0.1f;
    //private float jumpForce = 2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private float playerSpeed = 10f;
    Vector3 velocity;
    Vector3 movement;
    Vector3 jump;
   


    void Start()
    {
        controller = GetComponent<CharacterController>();

    }




    void Update()
    {
        Movement();
        //AnimationPlayer();


    }

    void Movement() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }


        float z = Input.GetAxis("Vertical");

        if (z != 0)
        {
            playerAnimator.SetBool("walking", true);
        }
        else
        {
            playerAnimator.SetBool("walking", false);
        }

        movement = transform.forward * z;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        controller.Move(movement * playerSpeed * Time.deltaTime);
        

        if (DetectButtonPressure.backPressed)
        {
            float zMov = -1;
            movement = transform.forward * zMov;
            controller.Move(movement * playerSpeed * Time.deltaTime);
        }
        if (DetectButtonPressure.forwardPressed)
        {
            float zMov = +1;
            movement = transform.forward * zMov;
            controller.Move(movement * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || DetectButtonPressure.leftPressed)
        {
            transform.Rotate(-Vector3.up * 90f * Time.deltaTime * 2f);
        }
        if (Input.GetKey(KeyCode.D) || DetectButtonPressure.rightPressed)
        {
            transform.Rotate(Vector3.up * 90f * Time.deltaTime * 2f);
        }

        /* if (isGrounded && Input.GetKeyDown(KeyCode.Space))
         {
             velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

             //Debug.Log("1 salto");
             //playerAnimator.SetBool("Jump", true);

         }
        */



    }

   
}





/*
    void AnimationPlayer(){
         if (Input.GetKeyDown(KeyCode.W)) 
        {
            playerAnimator.SetBool("Run", true);
        }
        if(Input.GetKeyUp(KeyCode.W)){
            playerAnimator.SetBool("Run", false);
        }

        if(Input.GetKey(KeyCode.A)){
            //playerAnimator.SetBool("Run", true);
            transform.Rotate(-Vector3.up * 90f * Time.deltaTime*2f);
        }
        if(Input.GetKey(KeyCode.D)){
            //playerAnimator.SetBool("Run", true);
            transform.Rotate(Vector3.up * 90f * Time.deltaTime*2f);
        }

        if(Input.GetKeyUp(KeyCode.A)){
           // playerAnimator.SetBool("Run", false);
        }

        if(Input.GetKeyUp(KeyCode.D)){
            //playerAnimator.SetBool("Run", false);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            playerAnimator.SetBool("Run Backward", true);
        }
        if(Input.GetKeyUp(KeyCode.S)){
            playerAnimator.SetBool("Run Backward", false);
        }
    }
*/


