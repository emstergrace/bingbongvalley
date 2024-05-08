using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingInteractable : MonoBehaviour, IInteractable
{
	public void Interact() {
		FishingController.Inst.InitializeFishing();
	}
}
