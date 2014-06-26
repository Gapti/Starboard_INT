using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(HoverText))]
public class DialogueRandom : MonoBehaviour {

	// Referenced dialogue
	public DialoguerDialogues dialogue;

	// Minimum time to trigger in seconds
	public float timeMin = 20;

	// Maximum time to trigger in seconds
	public float timeMax = 120;

	// Hover text instance of the object
	private HoverText hover;

	// Count down to appearance in seconds
	private float counter;


	// Pre-Initialization
	void Awake () {
		hover = this.gameObject.GetComponent<HoverText>();
	}


	// Initialization
	void Start () {
		ResetCounter();
	}


	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;

		if (counter <= 0) {
			TriggerDialogue();
			ResetCounter();
			counter += hover.length;
		}
	}


	// Starts counting down
	void ResetCounter() {
		counter = Random.Range(timeMin, timeMax);
	}


	// Triggers a randomized dialogue box
	void TriggerDialogue() {
		List<DialoguerCore.AbstractDialoguePhase> phases =
			DialoguerCore.DialoguerDataManager.GetDialogueById((int)dialogue).phases;

		int r = Random.Range(0, phases.Count - 1);

		DialoguerCore.TextPhase tp = phases[r] as DialoguerCore.TextPhase;
		if (tp != null) {
			hover.Display("\"" + tp.data.text + "\"", Color.white);
		}
	}
}
