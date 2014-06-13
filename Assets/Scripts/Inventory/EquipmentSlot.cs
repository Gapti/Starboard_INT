using UnityEngine;
using System.Collections;

public class EquipmentSlot : ItemSlot {

	public Equipment equipment;
	public SlotType slot;


	protected override Item observedItem {
		get {
			return (equipment != null) ? equipment.GetItem(slot) : null;
		}
	}

	protected override bool Replace (Item item)
	{
		return equipment.Equip(item, slot);
	}

	protected override bool PlayersInventoryCheck ()
	{
		return false;
	}

	protected override bool AddToPlayersInventory (Item item)
	{
		if(_playerInventory.Additem(item))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
