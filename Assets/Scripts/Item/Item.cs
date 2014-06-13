using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SlotType
{
	None,
	Helm,
	Chest,
	Legs,
	Hands,
	Feet,
	Weapon
}

public enum ItemType
{
	LaserWeapon,
	LegacyWeapon,
	MagneticWeapon,
	Melee,
	Armor,
	Misc,
	Consumable,
	Quest,
	Generator,
	Enhancer
}

[System.Serializable]
public class Item {
	public int id;
	public string ItemName;
	public  bool Stackable;
	public int MaxStack;
	public ItemType Type;
	public string ItemGameObject;
	public string Atlas;
	public string ItemSprite;
	public string Description;
	public SlotType Slot;

	[System.NonSerialized]
	public int StackAmount = 1;

	protected Item(){}

	protected Item (Item other)
	{
		this.id = other.id;
		this.ItemName = other.ItemName;
		this.Stackable = other.Stackable;
		this.MaxStack = other.MaxStack;
		this.Type = other.Type;
		this.ItemGameObject = other.ItemGameObject;
		this.Atlas = other.Atlas;
		this.ItemSprite = other.ItemSprite;
		this.Description = other.Description;
		this.Slot = other.Slot;

	}

	public virtual Item Clone()
	{
		return new Item(this);
	}

	public virtual void Use(){}

	public virtual string GetItemDescription()
	{
		return ("no info");
	}
	
}

