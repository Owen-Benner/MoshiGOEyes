﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    private CharacterController cc;

    public float moveSpeed;
    public float rotSpeed;
    public float sinkSpeed;

    private float hold;

    private bool forward;

    private Vector3 move;
    private Vector3 rotate;

    private Config config = null;
    private bool replay;

    // Start is called before the first frame update
    void Start()
    {
	cc = GetComponent<CharacterController>();
	move = Vector3.zero;
	rotate = Vector3.zero;

        config = GameObject.Find("Logic").GetComponent<Logic>().globalConfig;
        replay = config.replay;
        moveSpeed = config.playerMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
	// Calculate movement if free to move
	if(hold <= 0f)
	{
            float vert = Input.GetAxis("Vertical");
            if(forward){vert = 1f;}
            if(vert > 0f) // Only move forward
                move += Time.deltaTime * vert * moveSpeed *
                    cc.transform.forward;

            if(!forward)
                rotate += Time.deltaTime * Input.GetAxis("Horizontal") *
                    rotSpeed * Vector3.up;
            //else
                // Set rotation?
	}
	else
	{
            hold -= Time.deltaTime;
	}

	// Calculate lower to ground
	if (!cc.isGrounded)
	    move += Time.deltaTime * sinkSpeed * Vector3.down;

	// Apply character translation
	cc.Move(move);

	// Apply character rotation
	cc.transform.Rotate(rotate);

	// Reset vectors
	move = Vector3.zero;
	rotate = Vector3.zero;
    }

    public void BeginHold(float length)
    {
        hold = length;
    }

    public void EndHold()
    {
        hold = 0;
    }

    public bool IsHolding()
    {
        if(hold <= 0)
        {
            return false;
        }
        return true;
    }

    public void setForward(bool on)
    {
        forward = on;
    }

}
