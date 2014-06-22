using UnityEngine;
using System.Collections;

public class RightClickEquipButton : MonoBehaviour {

	void OnClick()
	{
		RightClickManager.instance.Equip();
	}
}
