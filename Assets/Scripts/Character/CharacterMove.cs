using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{
	public bool isRunning = true; // Walk/Run Toggle
	public float walkSpeed = 3.0f; // Character movement speed when walking.
	public float runSpeed = 6.0f; // Character movement speed when running.
	public float jumpSpeed = 8.0f; // The Jump speed
	public float gravity = 20.0f; // Gravity for the character.

	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero; // The move direction.

	void Start()
	{
		controller = transform.GetComponent<CharacterController>();			
	}

	void FixedUpdate()
	{
		if(!IsGrounded())
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}
		else
		{
			moveDirection.y = 0;
		}

		float speed = isRunning ? runSpeed : walkSpeed;
		Vector3 movement = new Vector3(moveDirection.x * speed,
		                               moveDirection.y,
		                               moveDirection.z * speed);
        
        controller.Move(movement * Time.deltaTime);		
	}

	public bool IsGrounded()
	{
		return controller.isGrounded;
	}
	
	public bool IsMoving()
	{
		return moveDirection.x != 0 || moveDirection.z != 0;
	}

	public void SetMoveDirection(float side, float forward)
	{
		moveDirection = new Vector3(side, moveDirection.y, forward);
		moveDirection = transform.TransformDirection(moveDirection);
	}
}
