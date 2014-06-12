using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMove))]
public class CharacterAnimation : MonoBehaviour
{
	public int numIdleBreakAnimations = 3;

	private CharacterMove characterMove;
	private Animator animator;
	
	void Start()
	{
		try
		{
			animator = transform.GetComponentInChildren<Animator>();						
		}
		catch(Exception e)
		{
			Debug.LogWarning("No animator attached to character." + e.Message);
		}

		characterMove = transform.GetComponent<CharacterMove>();
	}
	
	void Update()
	{
		if(animator != null)
		{
			animator.SetBool("Moving", characterMove.IsMoving());

			if(numIdleBreakAnimations > 0)
				animator.SetInteger("IdleBreakup", UnityEngine.Random.Range(1, numIdleBreakAnimations));

			animator.SetFloat("WalkRunToggle", characterMove.isRunning ? 1 : 0);
		}
	}
}
