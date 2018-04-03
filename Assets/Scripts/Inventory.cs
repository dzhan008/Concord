using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject {

	public ItemSO CurrentItem
	{
		get 
		{ 
			if (CurrentItemIndex == -1)
			{
				return null;
			}
			return inventory[CurrentItemIndex];
		}
		set {}
	}

	// Inventory with each item scriptable object and the int quantity held
	private List<ItemSO> inventory = new List<ItemSO>();
	private Dictionary<ItemSO, int> quantities = new Dictionary<ItemSO, int>();

	private int CurrentItemIndex = -1;
	[SerializeField] private int max_size = 3;

	public bool AddItem(Item new_item, PlayerInfo message_UI) 
	{
		if (inventory.Count < max_size && new_item.ItemData != null) {
			inventory.Add(new_item.ItemData);
			if (CurrentItemIndex == -1) {
				CurrentItemIndex = 0;
				message_UI.RefreshIventory();
			}

			// TODO: remove test
			string invStr = "Inventory: ";
			foreach (ItemSO i in inventory) { invStr += (i.name + ", "); }
			Debug.Log(invStr);

			return true;
		}
		message_UI.DisplayMessage("Inventory Full");
		return false;
	}
	
	public void ScrollLeft() {
		if (inventory.Count > 1)
		{
			if (--CurrentItemIndex < 0)
			{
				CurrentItemIndex = inventory.Count - 1;
			}
		}
	}

	public void ScrollRight() {
		//player's selected item = List index++ mod size
		if (inventory.Count > 1)
		{
			CurrentItemIndex = (CurrentItemIndex + 1) % inventory.Count;
		}
	}

}