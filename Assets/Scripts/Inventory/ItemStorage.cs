using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GUIGroups
{
	Inventory,
	MiniMap,
	Journal,
	Charactor,
	Menu
}

public interface IToggleGUI
{
	void ToggleMyGUI ();
}

[System.Serializable]
public class ItemStorage : MonoBehaviour, IToggleGUI {
	
	private bool _showGUI = false;

	public int MaxSlots;
	public string StorageName = "Inventory";
	public bool PlayersInventory;
	public GameObject StoragePrefab;
	public ItemDataBase Database;
	const GUIGroups GUIGroup = GUIGroups.Inventory;

	public Item[] Items;

	private GameObject _GUIRef;
	

	void Start()
	{

		Items = new Item[MaxSlots];

		Items[1] = Database.Get(ItemType.LegacyWeapon, 0);
		Items[2] = Database.Get(ItemType.LaserWeapon, 0);

		Items[3] = Database.Get(ItemType.Misc, 0);
		Items[4] = Database.Get(ItemType.Misc, 0);
		Items[5] = Database.Get(ItemType.Armor, 0);

		//ToggleMyGUI(GUIGroup);
	}


	public void ToggleMyGUI ()
	{
		if(_showGUI)
		{
			NGUITools.Destroy(_GUIRef);
		}
		else
		{
			MakeGUI();
		}

		_showGUI =! _showGUI;
	}

	void MakeGUI ()
	{
		GameObject GUIRoot = GameObject.FindGameObjectWithTag("UIRoot");
		_GUIRef = NGUITools.AddChild(GUIRoot, StoragePrefab);
		StorageSlotMaker s = _GUIRef.GetComponent<StorageSlotMaker>();
		s.BuildSlots(MaxSlots, this, StorageName);
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


	public Item GetItem (int slot) { return (slot < Items.Length) ? Items[slot] : null; }

	void Update()
	{
//		if(Input.GetKeyDown(KeyCode.I))
//		{
//			if(_showGUI)
//			{
//
//				Component[] components  = GetComponents<Component>();
//
//				foreach(Component component in components)
//				{
//					var temp = component as IToggleGUI;
//					if(temp != null) temp.ToggleMyGUI(GUIGroups.Inventory);
//				}
//			}
//			else if(PlayersInventory)
//			{
//				ToggleMyGUI(GUIGroups.Inventory);
//			}
//		}
	}
}
