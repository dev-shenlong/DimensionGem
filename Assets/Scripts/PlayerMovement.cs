using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    public float RunSpeed = 40f;
    float dirX;
    float dirY;
    bool jump  = false;
    bool crouch = false;
    private enum MovementState { idle,running,jumping,falling}
    private Animator aanim;
    private Rigidbody2D rb;
    

    // Update is called once per frame
    [SerializeField] private bool isCharge = false;
    [SerializeField] private float maxJumpCharge = 500f;
    [SerializeField] private float JumpXSpeed = 60f;

     void Start()
    {
        aanim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()

    {
        //dirX = Input.GetAxisRaw("Horizontal");
        Debug.Log(controller.charge_Jumpforce);

       if(!isCharge)
        {
            dirX = Input.GetAxisRaw("Horizontal") * RunSpeed;
        }
        dirY = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && controller.charge_Jumpforce<maxJumpCharge)
        {
            jump = false;
            isCharge = true;
        }
        if(isCharge)
        {
            controller.charge_Jumpforce += 9;
        }
        if(Input.GetButtonUp("Jump") || controller.charge_Jumpforce >=  maxJumpCharge )
        {
            jump = true;
            isCharge = false;
            dirX = Input.GetAxisRaw("Horizontal") * JumpXSpeed;

        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }

        UpdateAnimation();


    }
    private void FixedUpdate()
    {
        //move our character
        controller.Move(dirX*Time.fixedDeltaTime, crouch, jump);
        jump = false;  
    }
    private void UpdateAnimation()
    {
        MovementState state;
         
        if (dirX > 0f)
        {
            state = MovementState.running;
            
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
           
        }
        else
        {
            state = MovementState.idle;
        }



        if (rb.velocity.y>0.2f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.2f){
            state = MovementState.falling;

        }
        aanim.SetInteger("state", (int)state); 

        
    }

}
