using UnityEngine;
using System.Collections;

public class RightClickUnequipButton : MonoBehaviour {

	void OnClick()
	{
		RightClickManager.instance.Unequip();
	}
}
