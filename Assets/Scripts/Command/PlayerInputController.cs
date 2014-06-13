using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICommand
{
	void ExecuteCommand();
}


/// <summary>
/// invoker class
/// </summary>
/// 
public class PlayerInputController : MonoBehaviour
{
	public List<KeyValuePair<KeyCode, ICommand>> InputCommands =  new List<KeyValuePair<KeyCode, ICommand>>();


	void Start()
	{
		InputCommands.Add(new KeyValuePair<KeyCode, ICommand>(KeyCode.I, new OpenInventoryCommand(transform.gameObject)));
		InputCommands.Add(new KeyValuePair<KeyCode, ICommand>(KeyCode.C, new OpenCharacterCommand(transform.gameObject)));
	}

	// Update is called once per frame
	void Update () {
		foreach (var item in InputCommands) 
		{
			if(Input.GetKeyDown(item.Key))
			{
				item.Value.ExecuteCommand();
			}
		}
	}
}

public class OpenInventoryCommand : ICommand
{
	public GameObject InventoryOwner;

	public OpenInventoryCommand(GameObject inventoryOwner)
	{
		InventoryOwner = inventoryOwner;
	}

	public void ExecuteCommand()
	{
		InventoryOwner.GetComponent<ItemStorage>().ToggleMyGUI();
	}
}

public class OpenCharacterCommand : ICommand
{
	public GameObject CharacterOwner;

	public OpenCharacterCommand(GameObject characterOwner)
	{
		CharacterOwner = characterOwner;
	}

	public void ExecuteCommand()
	{
		CharacterOwner.GetComponent<Equipment>().ToggleMyGUI();
	}
}
