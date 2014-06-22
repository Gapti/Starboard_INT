using UnityEngine;
using System.Collections;

public class MakeEquipment : MonoBehaviour {

	public EquipmentSlot[] slots;
	public Equipment equipment;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < slots.Length; i++) 
		{
			slots[i].equipment = equipment;
		}
	}

	public void CloseMyWindow()
	{
		equipment.ToggleMyGUI ();
		RightClickManager.instance.Clear ();
	}

}
