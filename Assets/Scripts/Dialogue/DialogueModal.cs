using UnityEngine;
using System.Collections;

public class DialogueModal : MonoBehaviour {
	
	public DialoguerDialogues dialogue;

	// Update is called once per frame
	void Update () {
		// TODO: Change how we want to start an NPC conversation

		// Left click on NPC starts dialogue
		if (Input.GetMouseButtonDown(0)) {
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
