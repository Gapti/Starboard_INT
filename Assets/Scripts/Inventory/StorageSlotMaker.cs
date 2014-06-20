using UnityEngine;
using System.Collections;

public class StorageSlotMaker : MonoBehaviour {

	public UIGrid StorageGrid;
	public UILabel TitleLabel;
	public GameObject StorageSlotTemplatePrefab;
	private ItemStorage _itemStorage;

	private ItemStorage SetItemStorage
	{
		set
		{
			_itemStorage = value;
		}
	}

	public void CloseMyWindow()
	{
		_itemStorage.ToggleMyGUI ();
	}

	public void BuildSlots(int SlotAmount, ItemStorage storage, string windowTitle)
	{

		SetItemStorage = storage;

		for(int a = 0; a < SlotAmount; a++)
		{
			TitleLabel.text = windowTitle;

			GameObject slot = NGUITools.AddChild(StorageGrid.gameObject, StorageSlotTemplatePrefab);
			slot.transform.name = a.ToString("000");
			StorageSlot s = slot.GetComponent<StorageSlot>();
			s.SlotID = a;
			s.inventory = storage;

		}
	}

}
