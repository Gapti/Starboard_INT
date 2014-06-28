using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	public Item[] items;
	public GameObject EquipmentPrefab;
	public GameObject _GUIRef;
	private bool _showGUI = false;
	const GUIGroups GUIGroup = GUIGroups.Character;

	void Start()
	{
		items = new Item[7];
	}

	public void ToggleMyGUI ()
	{
		if(_showGUI)
		{
			NGUITools.Destroy(_GUIRef);
		}
		else
		{
			GameObject GUIRoot = GameObject.FindGameObjectWithTag("UIRoot");
			_GUIRef = NGUITools.AddChild(GUIRoot, EquipmentPrefab);
			_GUIRef.GetComponent<MakeEquipment>().equipment = this;
		}

		_showGUI =! _showGUI;
	}
	
	public Item GetItem (SlotType slot)
	{
		if (slot != SlotType.None)
		{
			int index = (int)slot - 1;
			
			if (items != null && index < items.Length)
			{
				return items[index];
			}
		}
		return null;
	}

	public bool Equip(Item item, SlotType slot)
	{
		int index = (int)slot - 1;

		if(item == null)
		{
			items[index] = item;
			return true;
		}


		if(item.Slot == slot)
		{
			if(items[index] != null)
				return false;

			items[index] = item;

			return true;
		}

		return false;
	}

	public bool RightClickEquip(Item item)
	{

		if (item.Slot != SlotType.None && (int)item.Slot <= (int)SlotType.Weapon && items[(int)item.Slot -1] == null) 
		{
			items[(int)item.Slot -1] = item;
			return true;
		}

		return false;
	}
	
}
