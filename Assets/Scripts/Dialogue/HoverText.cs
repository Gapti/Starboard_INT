using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class HoverText : MonoBehaviour {

	// Life span of the text in seconds
	public float length = 6.0f;
	// Fade in time in seconds
	public float fadeIn = 0.5f;
	// Fade out time in seconds
	public float fadeOut = 0.5f;

	// NGUI root object name
	public string guiRootObjectName = "Dialogue Window";

	// Offset in world space coords
	public float yOffset = 0.1f; 

	// Window object
	private GameObject window;
	private GameObject hover;
	private UILabel label;

	// Countdown before disappearing
	private float counter;
	

	// Use this for initialization
	void Awake () {
		window = GameObject.Find(guiRootObjectName);

		// instantiate the hover text box
		if (hover == null) {
			hover = (GameObject)Instantiate(Resources.Load<GameObject>("HoverText"));
			hover.transform.parent = window.transform;
			hover.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			hover.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			
			foreach (Transform child in hover.transform)
			{
				if (child.name == "Label") {
					label = child.GetComponent<UILabel>();
				}
			}
			// hide it
			hover.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!hover.activeSelf)
			return;

		counter -= Time.deltaTime;

		// Fade in effect
		label.alpha = Mathf.Min(1.0f, (length - counter) / fadeIn);

		// Fade out effect
		if (counter <= fadeOut) {
			label.alpha = Mathf.Max(0.0f, counter / fadeOut);
		}

		// Calculate the position
		Vector3 pos = this.gameObject.transform.position;
		pos.y += this.gameObject.GetComponent<Collider>().bounds.extents.y + yOffset;

		Vector3 newPos = Camera.main.WorldToScreenPoint(pos);
		newPos.x -= Screen.width * 0.5f;
		newPos.y -= (Screen.height - label.height) * 0.5f;
		hover.transform.localPosition = new Vector3(newPos.x, newPos.y, 0.0f);

		// Hide when time expires
		if (counter <= 0.0f)
			hover.SetActive(false);
	}

	
	/// <summary>
	/// Display the specified text.
	/// </summary>
	/// <param name="text">Text.</param>
	/// <param name="color">Color of the text.</param>
	/// <param name="length">Length of the effect in seconds.</param>
	public void Display(string text, Color color) {
		counter = length;
		label.text = text;
		label.color = color;
		hover.SetActive(true);
	}
}
