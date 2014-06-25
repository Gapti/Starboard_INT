using UnityEngine;

using System.Collections;

public class Temp : MonoBehaviour {
	public float moveSpeed;
	public float rotateSpeed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("w")) {
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey("s")) {
			transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey("a")) {
			transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
		}
		
		if(Input.GetKey("d")) {
			transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
		}
	}
}
