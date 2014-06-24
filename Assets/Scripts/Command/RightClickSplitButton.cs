using UnityEngine;
using System.Collections;

public class RightClickSplitButton : MonoBehaviour {

	void OnClick()
	{
		RightClickManager.instance.Split();
	}
}
