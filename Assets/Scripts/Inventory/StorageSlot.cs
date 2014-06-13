using UnityEngine;
using System.Collections;

public class StorageSlot : ItemSlot {

	public int SlotID;
	public ItemStorage inventory;

	protected override Item observedItem {
		get {
				return (inventory != null) ? inventory.GetItem(SlotID) : null;
			}
	}

	protected override bool Replace (Item item)
	{
		return inventory.Add(SlotID, item);
	}

	protected override bool PlayersInventoryCheck ()
	{
		return inventory.PlayersInventory ? true : false;
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