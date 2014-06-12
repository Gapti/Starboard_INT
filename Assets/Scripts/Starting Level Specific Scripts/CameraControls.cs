using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	public string runWalkToggle, resetCamera, rotateCamera, determineCharacterDirection, Forwards, Backwards, Left, Right, notes;
	private string cameraControlsTextArea;

	// Sets cameraControlsTextField's value to be displayed in the text field
	void Start () {
		cameraControlsTextArea = "Run/walk toggle: " + runWalkToggle + "\r\nReset camera: " + resetCamera + 
									"\r\nRotate around the character: " + rotateCamera + 
									"\r\nDetermine character's direction (moving): " + determineCharacterDirection + 
									"\r\nMovement controls: " + Forwards + Left + Backwards + Right +
									"\r\nZoom in and out: " + "Scroll wheel" + "\r\n\r\n" + notes;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Displays the text field with the value of cameraControlsTextField
	void OnGUI() {
		cameraControlsTextArea = GUI.TextArea (new Rect(0, 100, 279, 330), cameraControlsTextArea);
	}
}
