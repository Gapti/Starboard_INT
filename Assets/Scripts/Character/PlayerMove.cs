using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : CharacterMove
{
	void Update()
	{
		if(IsGrounded())
		{
			float xMovement = Input.GetAxis("Horizontal");// The horizontal movement.
			float zMovement = Input.GetAxis("Vertical");// The vertical movement.

			// Mouse movement holding both buttons
			if(Input.GetButton("Fire1") && Input.GetButton("Fire2"))
				zMovement = 1;
				
			float tempAngle = Mathf.Atan2(zMovement, xMovement);
			xMovement *= Mathf.Abs(Mathf.Cos(tempAngle));
			zMovement *= Mathf.Abs(Mathf.Sin(tempAngle));

			SetMoveDirection(xMovement, zMovement);

			if(Input.GetButtonDown("Walk Run Toggle"))
				isRunning = !isRunning;
       	}
	}
}

