using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ARPG camera controller.
/// Created By: Juandre Swart 
/// Email: info@3dforge.co.za
/// 
/// A Script for a ARPG (Diablo, Path of Exile, Torchlight) style camera that also allows rotation on the y and x axis.
/// It contains code that makes objects transparent if they are between the camera and target.
/// The objects material needs to be a transparent shader so we can change the alpha value.
/// </summary>
public class CameraController : MonoBehaviour
{
	public Transform target;
	public float targetHeight = 1.0f; // The amount from the target object pivot the camera should look at.
    
	public float startingDistance = 10f; // Distance the camera starts from target object.
	public float maxDistance = 20f; // Max distance the camera can be from target object.
	public float minDistance = 3f; // Min distance the camera can be from target object.
	public float zoomSpeed = 20f; // The speed the camera zooms in.

	public float camRotationSpeed = 70;// The speed at which the camera rotates.
	public float camPitch = 45.0f; // The camera x euler angle.

	public bool fadeObjects = false; // Enable objects of a certain layer to be faded.
	public List<int> layersToTransparent = new List<int>();	// The layers where we will allow transparency.
	public float alpha = 0.3f; // The alpha value of the material when player behind object.


	private float camYaw = 0.0f; // The camera y euler angle.
	private Transform camTransform;
	private Transform prevHit;
	private float minCameraPitch = 30.0f; // The min angle on the camera's x axis.
	private float maxCameraPitch = 85.0f; // The max angle on the camera's x axis.
	private PlayerMove targetPlayerMove;
	private bool rotatingAndMoving = false;
	private float rotatingAndMovingOffset;

	void Start()
	{
		camTransform = transform;
		if(target == null)
		{			
			Debug.LogError("No target added, please add target Game object");
		}
		else
		{
			camTransform.position = target.position;	
			
			// Set default y angle.
			camYaw = target.rotation.eulerAngles.y;		
			
			targetPlayerMove = target.GetComponent<PlayerMove>();
			if(targetPlayerMove == null)
			{
				Debug.LogError("No PlayerMove component found, please attach one to the target Game Object");
			}
		}
	}
	
	void LateUpdate()
	{
		if(target == null || targetPlayerMove == null) return;

		UpdateZoom();
		UpdatePositionRotation();
		UpdateVisibility();
	}

	void UpdateZoom()
	{
		// Zoom Camera and keep the distance between [minDistance, maxDistance].
		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
		if(mouseWheel > 0)
		{			
			startingDistance -= Time.deltaTime * zoomSpeed;
			if(startingDistance < minDistance)
				startingDistance = minDistance;
		}
		else if(mouseWheel < 0)
		{
			startingDistance += Time.deltaTime*zoomSpeed;
            if(startingDistance > maxDistance)
                startingDistance = maxDistance;
        }
    }

	void UpdatePositionRotation()
	{
		// Rotate Camera around character.
		if(Input.GetButton("Fire3") || (Input.GetButton("Fire1") && Input.GetButton("Fire2"))|| (targetPlayerMove != null && targetPlayerMove.IsMoving()&& Input.GetButton("Fire1")))

		{
			float h = Input.GetAxis("Mouse X"); // The horizontal movement of the mouse.						
			float v = Input.GetAxis("Mouse Y"); // The vertical movement of the mouse.
			if(h > 0 && h > Math.Abs(v))
			{
				camTransform.RotateAround(target.transform.position, new Vector3(0, 1, 0), camRotationSpeed * Time.deltaTime);	
				camYaw = camTransform.eulerAngles.y;
			}
			else if(h < 0 && h < -Math.Abs(v))
			{
				camTransform.RotateAround(target.transform.position, new Vector3(0, 1, 0), -camRotationSpeed * Time.deltaTime);
				camYaw = camTransform.eulerAngles.y;
			}
			else if(v > 0 && v > Math.Abs(h))
			{
				camPitch += camRotationSpeed * Time.deltaTime;
				if(camPitch > maxCameraPitch)
				{
					camPitch = maxCameraPitch;
				}
			}
			else if(v < 0 && v < -Math.Abs(h))
			{
				camPitch += -camRotationSpeed * Time.deltaTime;
				if(camPitch < minCameraPitch)
				{
					camPitch = minCameraPitch;
				}
			}

			// Update target rotation
			if(targetPlayerMove != null && targetPlayerMove.IsMoving())
			{
				if(!rotatingAndMoving)
				{
					rotatingAndMoving = true;
					rotatingAndMovingOffset = camYaw - target.rotation.eulerAngles.y;
				}

				Quaternion tempRotation = Quaternion.Euler(camPitch, camYaw - rotatingAndMovingOffset, 0);
				tempRotation.x = 0;
				tempRotation.z = 0;
	            target.rotation = tempRotation;
			}
			else
			{
				rotatingAndMoving = false;
			}
		}
		else
		{
			rotatingAndMoving = false;

			if(targetPlayerMove != null && targetPlayerMove.IsMoving())
			{
				float targetAngle = target.rotation.eulerAngles.y;

				camYaw -= targetAngle;
				if(camYaw < 0)
					camYaw += 360;

				if(camYaw < 180)
				{
					camYaw += Time.deltaTime * -camRotationSpeed;
					if(camYaw < 0)
						camYaw = 0;
				}
				else
				{
					camYaw += Time.deltaTime * camRotationSpeed;
					if(camYaw >= 360)
						camYaw = 0;
				}

				camYaw += targetAngle;
				if(camYaw >= 360)
					camYaw -= 360;
			}
		}

		if(Input.GetButton("Fire4"))
			camYaw = target.rotation.eulerAngles.y;
        
        // Set camera angles.
        Quaternion rotation = Quaternion.Euler(camPitch, camYaw, 0); 	
        camTransform.rotation = rotation;

        // Position Camera.
        Vector3 trm = rotation * Vector3.forward * startingDistance + new Vector3(0, -1 * targetHeight, 0);
        Vector3 position = target.position - trm;
        camTransform.position = position;
    }

	void UpdateVisibility()
	{
		//Start checking if object between camera and target.
		if(fadeObjects)
		{
			// Cast ray from camera.position to target.position and check if the specified layers are between them.
			Ray ray = new Ray(camTransform.position, (target.position - camTransform.position).normalized);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, maxDistance))
			{
				Transform objectHit = hit.transform;
				if(layersToTransparent.Contains(objectHit.gameObject.layer))
				{
					if(prevHit != null)
					{
						prevHit.renderer.material.color = new Color(1, 1, 1, 1);
					}
					if(objectHit.renderer != null)
					{
						prevHit = objectHit;
						// Can only apply alpha if this material shader is transparent.
						prevHit.renderer.material.color = new Color(1, 1, 1, alpha);
					}
				}
				else if(prevHit != null)
				{
					prevHit.renderer.material.color = new Color(1, 1, 1, 1);
					prevHit = null;
				}
			}
		}
	}
}
