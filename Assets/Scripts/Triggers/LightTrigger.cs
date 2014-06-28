using UnityEngine;


namespace Starboard {

	/// <summary>
	/// Trigger to turn lights on/off
	/// </summary>
	/// <example>
	/// Put the prefab into the scene and attach lights in the inspector.
	/// Set the collider type and size to cover the area that you want to cause the trigger.
	/// </example>
	public class LightTrigger : Trigger {
		// Affected lights
		public Light[] lights;


		void Awake() {
			SetActive(false);
		}


		protected override void TriggerOn(Collider other) {
			SetActive(true);
		}


		protected override void TriggerOff(Collider other) {
			SetActive(false);
		}


		// Turns connected lights on/off
		public void SetActive(bool active) {
			foreach (Light l in lights) {
				l.enabled = active;
			}
		}
	}

}