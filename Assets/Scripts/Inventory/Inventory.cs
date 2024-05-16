using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
	public static Inventory Inst { get; private set; }

	public Dictionary<ItemData, int> ItemsList { get; private set; } = new Dictionary<ItemData, int>();

	public Action<ItemData> UpdateItem;

	private void Awake() {
		Inst = this;
	}

	public bool HasItem(ItemData newItem) {
		return ItemsList.ContainsKey(newItem) && ItemsList[newItem] > 0;
	}

	public void AddItem(ItemData newItem, int amount = 1) {
		if (!ItemsList.ContainsKey(newItem)) {
			ItemsList.Add(newItem, 0);
		}
		ItemsList[newItem] += amount;
		UpdateItem?.Invoke(newItem);
		EventManager.TriggerObjective(Objective.ItemNotifier + newItem.name, amount);
	}

	public void RemoveItem(ItemData newItem, int amount = 1) {
		if (ItemsList.ContainsKey(newItem) && ItemsList[newItem] > 0) {
			ItemsList[newItem] -= amount;
			if (ItemsList[newItem] < 0)
				ItemsList[newItem] = 0;
			UpdateItem?.Invoke(newItem);
			EventManager.TriggerObjective(Objective.ItemNotifier + newItem.name, amount);
		}
	}
}
