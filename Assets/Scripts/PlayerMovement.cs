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
    // Update is called once per frame
    [SerializeField] private bool isCharge = false;
    void Update()
    {
        Debug.Log(controller.charge_Jumpforce);

       if(!isCharge)
        {
            dirX = Input.GetAxisRaw("Horizontal") * RunSpeed;
        }
        dirY = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && controller.charge_Jumpforce<1000)
        {
            jump = false;
            isCharge = true;
        }
        if(isCharge)
        {
            controller.charge_Jumpforce += 10;
        }
        if(Input.GetButtonUp("Jump") || controller.charge_Jumpforce >=   1000 )
        {
            jump = true;
            isCharge = false;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }
            
    }
    private void FixedUpdate()
    {
        //move our character
        controller.Move(dirX*Time.fixedDeltaTime, crouch, jump);
        jump = false;  
    }

}
