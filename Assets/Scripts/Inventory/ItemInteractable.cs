using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemInteractable : MonoBehaviour, IInteractable
{
	[SerializeField] private ItemData itemType;
	public bool DestroyOnPickup = true;
	bool hasBeenPickedup = false;

	public Action PickedUp;

	public void Interact() {
		if (itemType == null) {
			Debug.LogError("Item data is null.");
			return;
		}
		if (hasBeenPickedup) return;
		hasBeenPickedup = true;
		Inventory.Inst.AddItem(itemType);

		//NotificationWindow.Inst.AddNotification("Picked up " + itemType + "!");

		PickedUp?.Invoke();
		if (DestroyOnPickup) {
			Destroy(this.gameObject);
		}
	}
}
