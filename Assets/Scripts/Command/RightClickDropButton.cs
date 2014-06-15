using UnityEngine;
using System.Collections;

public class RightClickDropButton : MonoBehaviour {

	void OnClick()
	{
		RightClickManager.instance.Drop();
	}
}
