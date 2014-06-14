using UnityEngine;
using System.Collections;

public class RightClickMaker : MonoBehaviour {

	public GameObject UseButton;
	public GameObject DropButton;
	public GameObject EquipButton;
	public GameObject SplitButton;


	public void Make(Item item)
	{
		switch(item.Type)
		{
		case ItemType.Armor:
			SetArmorButtons(item);
			break;
		case ItemType.Consumable:
			SetConsumable(item);
			break;
		case ItemType.Enhancer:
			SetEnhancer(item);
			break;
		}
	}

	void SetArmorButtons (Item item)
	{
		throw new System.NotImplementedException ();
	}

	void SetConsumable (Item item)
	{
		throw new System.NotImplementedException ();
	}

	void SetEnhancer (Item item)
	{
		throw new System.NotImplementedException ();
	}
}
