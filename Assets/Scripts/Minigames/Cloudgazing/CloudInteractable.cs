using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudInteractable : MonoBehaviour, IInteractable
{
	public void Interact() {
		CloudController.Inst.InitCloudGazing();
	}
}
