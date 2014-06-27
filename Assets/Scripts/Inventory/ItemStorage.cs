using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GUIGroups
{
	Inventory,
	MiniMap,
	Journal,
	Character,
	Menu
}

public interface IToggleGUI
{
	void ToggleMyGUI ();
}

[System.Serializable]
public class ItemStorage : MonoBehaviour, IToggleGUI {
	
	public int MaxSlots;
	public string StorageName = "Inventory";
	public bool PlayersInventory;
	public GameObject StoragePrefab;
	public ItemDataBase Database;
	const GUIGroups GUIGroup = GUIGroups.Inventory;

	[HideInInspector]
	public Item[] Items;

	public int[] StartingItemIDs;
	public ItemType[] StartingItemTypes;
	
	private GameObject _GUIRef;


	void Awake() {
		GameObject GUIRoot = GameObject.FindGameObjectWithTag("UIRoot");
		_GUIRef = NGUITools.AddChild(GUIRoot, StoragePrefab);
		
		Items = new Item[MaxSlots];
	}


	void Start()
	{
		// Setup starting items

		if (StartingItemIDs.Length != 0 || StartingItemTypes.Length != 0)
		{
			for (int i = 0; i < StartingItemIDs.Length && i < StartingItemTypes.Length; i++) 
			{
				Items [i] = Database.Get (StartingItemTypes [i], StartingItemIDs [i]);
			}
		}

		// Build inventory slots
		StorageSlotMaker s = _GUIRef.GetComponent<StorageSlotMaker>();
		s.BuildSlots(MaxSlots, this, StorageName);
		
		if (this.gameObject.tag == "MainPlayer") 
		{
			s.AddMoneyText();
		}
	}

	public void ToggleMyGUI ()
	{
		_GUIRef.SetActive(!_GUIRef.activeSelf);
	}


	public bool Additem(Item item)
	{
		for (int i = 0; i < Items.Length; i++) 
		{
			if(Items[i] == null)
			{
				Add(i, item);
				return true;
			}
		}
		
		return false;
	}
	
	
	public bool Add(int pos, Item item)
	{
		///passing a none item
		if(item == null)
		{
			Items[pos] = item;

			return true;
		}

		///check for item postion is empty
		if(Items[pos] == null)
		{
			Items[pos] = item;
			
			return true;
		}

		//check to see if the item is stackable
		if(item.Stackable)
		{ 
			if(Items[pos].ItemName == item.ItemName && Items[pos].StackAmount < Items[pos].MaxStack)
			{
				if((Items[pos].StackAmount + item.StackAmount) > Items[pos].MaxStack)
				{
					int tempStackAmount = Items[pos].MaxStack - Items[pos].StackAmount;

					Items[pos].StackAmount += tempStackAmount;
					item.StackAmount -= tempStackAmount;

					return false;
				}

				Items[pos].StackAmount += item.StackAmount;
				
				return true;
			}
		}

		///check if there is room
		int slotId = CheckForSpace(item);
		
		if(slotId == -1)
		{
			//no room
			return false;
		}
		else
		{
			//add item in the room slot
			Add(slotId, item);
			return true;
		}
		
	}


	int CheckForSpace (Item item)
	{
		for (int a = 0; a < this.Items.Length; a++) 
		{
			if (Items[a] == null) 
			{
				return a;
			}
		}
		
		return -1;
	}


	public void Split(Item item, int pos, int amount)
	{
		int NewItemStackAmount = amount;
		int OldItemStackAmount = Items [pos].StackAmount - amount;

		Item SplitItem = item.Clone ();
		SplitItem.StackAmount = NewItemStackAmount;

		if (Additem (SplitItem)) 
		{
			Items[pos].StackAmount = OldItemStackAmount;
		}
	}


	public Item GetItem (int slot) { return (slot < Items.Length) ? Items[slot] : null; }
	
}
