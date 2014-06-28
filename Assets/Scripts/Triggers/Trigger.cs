using UnityEngine;
using System.Linq;


namespace Starboard {

	[RequireComponent(typeof(Collider))]
	public class Trigger : MonoBehaviour {

		// Tags that the trigger reacts to
		public string[] tags = { "Player", "Companion", "NPC", "Enemy" };

		// Current number of tagged objects inside the trigger
		private int count = 0;

		// Current on/off state
		public bool State {
			get {
				return count > 0;
			}
		}


		//------- Position hack
		/*private Vector3 positionHack;

		void Awake() {
			positionHack = gameObject.transform.position;
			gameObject.transform.position = gameObject.transform.position + Random.insideUnitSphere * 10000f;
		}
		void Start() {
			gameObject.transform.position = positionHack;
		}*/
		//-------


		// Executes when a collider enters this object's collider
		void OnTriggerEnter(Collider other) {
			// Execute on specific tags only
			if (tags.Contains(other.tag)) {
				// If this is the first entering object
				if (count++ <= 0) {
					TriggerOn(other);
				}

				TriggerEnter(other);
			}
		}

		// Executes when a collider leaves this object's collider
		void OnTriggerExit(Collider other) {
			// Execute on specific tags only
			if (tags.Contains(other.tag)) {
				TriggerExit(other);

				// When there are no more relevant objects inside
				if (--count <= 0) {
					TriggerOff(other);
				}
			}
		}


		// Called when a new tagged instance enters
		protected virtual void TriggerEnter(Collider other) {}
		// Called when a tagged instance leaves
		protected virtual void TriggerExit(Collider other) {}
		// Called when a first tagged instance enters
		protected virtual void TriggerOn(Collider other) {}
		// Called when a last tagged instance leaves
		protected virtual void TriggerOff(Collider other) {}
	}

}
