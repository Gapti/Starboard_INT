using UnityEngine;
using System.Collections;


/// <summary>
/// A modal dialogue. Gets triggered when tied object is clicked.
/// </summary>
/// <example>
/// Drag onto an NPC GameObject and choose the correct dialogue in the editor.
/// </example>
public class DialogueModal : MonoBehaviour {

	// ID of the Dialoguer dialogue
	public DialoguerDialogues dialogue;

	// Update is called once per frame
	void Update () {
		// TODO: Change how we want to start an NPC conversation

		// Left click on NPC starts dialogue
		if (Input.GetMouseButtonDown(0) && gameObject.renderer.isVisible) {
			// Raycast from camera to mouse position
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hitInfo)) {
				if (hitInfo.collider.gameObject == this.gameObject) {
					Dialoguer.StartDialogue(dialogue);
				}
			}
		}
	}



}
