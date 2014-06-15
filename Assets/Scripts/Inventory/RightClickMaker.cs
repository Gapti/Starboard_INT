using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RightClickMaker : MonoBehaviour {

	public GameObject UseButton;
	public GameObject DropButton;
	public GameObject EquipButton;
	public GameObject SplitButton;
	public GameObject UnequipButton;


	public void Make(ItemSlot itemSlot)
	{
		Item item = itemSlot.item;

		SetStackable(item);

		switch(item.Type)
		{
		case ItemType.Armor:
			SetArmorButtons(itemSlot);
			break;
		case ItemType.Consumable:
			SetConsumable(itemSlot);
			break;
		case ItemType.Enhancer:
			SetEnhancer(itemSlot);
			break;
		case ItemType.Generator:
			SetGenerator(itemSlot);
			break;
		case ItemType.LaserWeapon:
			SetLaserWeapon(itemSlot);
			break;
		case ItemType.LegacyWeapon:
			SetLegacyWeapon(itemSlot);
			break;
		case ItemType.MagneticWeapon:
			SetMagneticWeapon(itemSlot);
			break;
		case ItemType.Melee:
			SetMelee(itemSlot);
			break;
		case ItemType.Misc:
			SetMisc(itemSlot);
			break;
		case ItemType.Quest:
			SetQuest(itemSlot);
			break;
		}
	}

	void SetStackable(Item item)
	{
		if(item.Stackable)
		{
			if(item.StackAmount > 1)
			{
				SplitButton.SetActive(true);
				return;
			}
		}

		SplitButton.SetActive(false);
	}

	void SetEquipStatus(ItemSlot itemSlot)
	{

		var type = itemSlot.GetType();

		if(type == typeof(StorageSlot))
		{
			EquipButton.SetActive(true);
			UnequipButton.SetActive(false);
		}
		else if(type == typeof(EquipmentSlot))
		{
			EquipButton.SetActive(false);
			UnequipButton.SetActive(true);
		}
		else
		{
			EquipButton.SetActive(false);
			UnequipButton.SetActive(true);
		}
	}

	void SetArmorButtons (ItemSlot itemSlot)
	{
		SetEquipStatus(itemSlot);
	}

	void SetConsumable (ItemSlot itemSlot)
	{
		throw new System.NotImplementedException ();
	}

	void SetEnhancer (ItemSlot itemSlot)
	{
		throw new System.NotImplementedException ();
	}

	void SetGenerator (ItemSlot itemSlot)
	{
		throw new System.NotImplementedException ();
	}

	void SetLaserWeapon (ItemSlot itemSlot)
	{
		SetEquipStatus(itemSlot);
	}

	void SetLegacyWeapon(ItemSlot itemSlot)
	{
		SetEquipStatus(itemSlot);
	}

	void SetMagneticWeapon (ItemSlot itemSlot)
	{
		SetEquipStatus(itemSlot);
	}

	void SetMelee (ItemSlot itemSlot)
	{
		SetEquipStatus(itemSlot);
	}

	void SetMisc (ItemSlot itemSlot)
	{
		throw new System.NotImplementedException ();
	}

	void SetQuest (ItemSlot itemSlot)
	{
		throw new System.NotImplementedException ();
	}
}
